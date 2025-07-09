# 🚀 API Test Automation Framework with RestSharp + NUnit (.NET)

This project demonstrates a clean, scalable, and maintainable approach to API test automation using **RestSharp** and **NUnit** in **.NET**. It includes token-based authentication, reusable client logic, and well-structured test cases that can be extended easily.

---

## 🛠 Tech Stack

- **.NET SDK** — Using 9 here
- **RestSharp** — For API calls
- **NUnit** — For test framework
- **C#**
- *(Coming soon)*: **Allure Reporting** for beautiful test reports

---

## 📂 Project Structure

```bash
ApiTestWithRestSharpNUnit/
│
├── RestSharpDemo/              # Main project
│   ├── ApiClient.cs            # Handles all API logic & reusable HTTP client
│   ├── AppConfig.cs            # Loads config values from appsettings.json
│   ├── appsettings.json        # Holds base URL and credentials
│   ├── ApiTests.cs             # NUnit test cases
│
├── .gitignore
├── README.md

```

---

## 🔐 Authentication Flow

We use a `POST` request to fetch an OAuth2 access token using client credentials:

- Endpoint: `test/api/oauth2/access_token`
- Headers:
  - `Content-Type: application/x-www-form-urlencoded`
  - `Authorization: Basic <base64(client_id:client_secret)>`

The token is reused for subsequent API calls.

---

## ✅ Implemented Tests

| Test Case | Description |
|-----------|-------------|
| `Authenticate_ShouldReturnToken()` | Verifies token generation from auth API |
| `GetProductListApi_ShouldReturnSuccess()` | Validates GET request with token |
| `GetProductManifest_ShouldReturnSuccess()` | Another GET with a request body in `application/json` |

---

## ▶️ How to Run

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Git

### Steps

1. Clone the repository:

```bash
git clone https://github.com/RakibInfolytx/ApiTestWithRestSharpNUnit.git
cd ApiTestWithRestSharpNUnit
cd RestSharpDemo
```

2. Run the tests:

```bash
dotnet test
```

You’ll see a summary like:

```text
Test summary: total: 3, failed: 0, succeeded: 3, skipped: 0
```

---

## 📊 Future Enhancements

- 🔧 Integrate **Allure Reporting**
- 🔁 Add test data from external sources (e.g. CSV or JSON)
- 🔍 Extend validations with schema & status assertions
- 🧪 CI integration (GitHub Actions or Azure Pipelines)

---

## 🤝 Contributing

Pull requests are welcome. For major changes, open an issue first to discuss what you’d like to change.

---

## 🧾 License

MIT License © [Rakibul Hasan](https://github.com/RakibInfolytx)

---

## 💬 Feedback

Have suggestions or improvements? Feel free to open an issue or reach out!

---
