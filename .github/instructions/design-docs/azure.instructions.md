---
applyTo: "**"
---

## Azure 利用方針

- 音声認識は Azure Speech Service を前提にすること。
- リアルタイム翻訳および高品質翻訳は Azure AI Translator を前提にし、必要な場合のみ Azure OpenAI の併用を検討すること。
- 話者識別は Azure Speech Service の diarization 機能を前提にすること。
- Azure サービスへの認証で API キーを使わないこと。

## Azure 認証方針

- Azure 認証には Azure.Identity の DefaultAzureCredential を使うこと。
- ただし認証経路はブラウザ認証だけに制限すること。
- DefaultAzureCredentialOptions で EnvironmentCredential、WorkloadIdentityCredential、ManagedIdentityCredential、VisualStudioCredential、VisualStudioCodeCredential、AzureCliCredential、AzurePowerShellCredential、AzureDeveloperCliCredential、BrokerCredential を除外すること。
- ExcludeInteractiveBrowserCredential は false に設定し、InteractiveBrowserCredential のみを有効にすること。
- トークン取得、更新、キャッシュは Azure.Identity に委譲し、アプリケーション側でアクセストークンやリフレッシュトークンを独自管理しないこと。
- Visual Studio、Azure CLI、Managed Identity など他の認証手段にフォールバックしないこと。
- 認証コードを提案する場合は、必ず DefaultAzureCredential を前提にし、API キー方式や独自トークン保存を提案しないこと。
- Azure SDK を使う場合は Azure.Identity と Azure.Core に沿った一般的な .NET 実装パターンを優先すること。
