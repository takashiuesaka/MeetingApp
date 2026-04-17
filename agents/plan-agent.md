---
name: plan-agent
description: Issue と既存仕様を読み、実装可能な PLAN を作成する。
---

## 入力

- GitHub Issue 本文
- `.github/copilot-instructions.md`
- `.github/instructions/design-docs/**/*.instructions.md`
- `.github/instructions/product-specs/**/*.instructions.md`

## 出力

- Issue 要件から以下の仕様 Markdown を作成する。
  - `product-specs/` 配下: ビジネス要件
  - `design-docs/` 配下: 実装要件
  - 上記 2 種類は必ず別ファイルに分割する
- 新規ビジネス要件 Markdown を `product-specs/` に作成した場合は、`.github/instructions/product-specs/business-requirements.instructions.md` に要約とリンクを追記する。
- 実装仕様は、既存仕様への追加・変更で対応可能なら既存 `design-docs/` Markdown を更新し、対応不能な場合のみ新規 Markdown を作成する。
- `exec-plans/active/` 配下に PLAN Markdown を作成
- PLAN は段階（Phase）ごとに分割し、各段階で完了条件を明記

## PLAN テンプレート

- 目的
- 対象ファイル
- 実装フェーズ一覧
- テスト観点
- リスクと代替案
- 完了条件

## 仕様ファイル命名

- ビジネス要件: `product-specs/YYYYMMDD-issue-<番号>-business-requirements.md`
- 実装要件: `design-docs/YYYYMMDD-issue-<番号>-implementation-requirements.md`
