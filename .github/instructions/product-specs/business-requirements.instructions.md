---
applyTo: "**"
---

## プロダクト方針

- このアプリは .NET MAUI を使った macOS デスクトップアプリケーションとして実装すること。
- 対象プラットフォームは macOS のみとすること。
- .NET は .NET 10 を前提とすること。
- 配布は framework-dependent とし、.NET Runtime をアプリに同梱しないこと。
- 公開コマンドでは --self-contained false を前提にすること。

## 機能要件

- MS Teams、Slack、PC マイクなど複数の音声ソースを扱えるようにすること。
- 会議音声をリアルタイムで文字起こしし、保存できるようにすること。
- リアルタイム文字起こし結果をリアルタイムで日本語へ翻訳すること。
- 会議全体の文脈を踏まえた、より正確な日本語翻訳を後処理で生成できるようにすること。
- 話者は匿名のまま識別し、同一セッション内では Speaker A、Speaker B のような一貫したラベルを維持すること。

## 品質要件

- リアルタイム文字起こしは低遅延を優先し、目標遅延は 2 秒以内とすること。
- リアルタイム翻訳は文字起こし受信後 1 秒以内に表示開始できる設計を優先すること。
- 音声データと書き起こしデータは、Azure API 呼び出しを除き外部送信しないこと。

## 仕様ドキュメント配置

- ビジネス要件・仕様に関する Markdown は `product-specs/` 配下へ配置すること。

## Issue 起点のビジネス要件更新ルール

- Issue から新しいビジネス要件を作成する場合は、`product-specs/` 配下に新しい Markdown を作成すること。
- 新しいビジネス要件 Markdown を作成した場合は、この `business-requirements.instructions.md` に要約（3〜5 行）を追記すること。
- 要約には、作成した新規 Markdown へのリンクを必ず含めること。
- 既存ビジネス要件の追加・変更で対応可能な場合は、新規作成ではなく既存 Markdown を更新すること。

## 追加ビジネス要件

### 実行環境変更（macOS のみ）

対象プラットフォームを Windows + macOS から macOS のみに変更。
ビルドターゲットは `net10.0-maccatalyst` のみとし、Windows 向け配布・パッケージングは廃止する。
→ 詳細: [product-specs/20260417-platform-change-business-requirements.md](../../../product-specs/20260417-platform-change-business-requirements.md)
