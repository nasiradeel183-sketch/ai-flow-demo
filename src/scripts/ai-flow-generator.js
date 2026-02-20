const fs = require("fs");
const axios = require("axios");

const apiKey = process.env.OPENAI_API_KEY;

async function generateFlow(code) {
  const response = await axios.post(
    "https://api.openai.com/v1/chat/completions",
    {
      model: "gpt-4o-mini",
      messages: [
        {
          role: "system",
          content: `
You are a senior software architect.
Analyze C# code and generate a Mermaid flowchart.
- Show decisions clearly
- Label branches Yes/No
- Handle switch statements
- Use business-friendly labels
Return only Mermaid flowchart syntax.
`
        },
        {
          role: "user",
          content: code
        }
      ]
    },
    {
      headers: {
        Authorization: `Bearer ${apiKey}`,
        "Content-Type": "application/json"
      }
    }
  );

  return response.data.choices[0].message.content;
}

async function main() {
  const code = fs.readFileSync("src/InvoiceProcessor.cs", "utf-8");

  const mermaid = await generateFlow(code);

  const mdContent = `
---
id: invoice-flow
title: Invoice Processing Flow (AI Generated)
---

# AI Generated Flow

\`\`\`mermaid
${mermaid}
\`\`\`
`;

  fs.writeFileSync(
    "flow-docs-site/docs/invoice-flow.md",
    mdContent
  );

  console.log("AI documentation generated.");
}

main();
