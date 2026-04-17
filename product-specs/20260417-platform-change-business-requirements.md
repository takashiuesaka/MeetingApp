# 実行環境変更 ビジネス要件

## 背景

当初は Windows と macOS の両プラットフォームを対象としていたが、運用・開発効率を考慮し **macOS のみ** を対象プラットフォームとする方針に変更する。

## 変更内容

| 項目 | 変更前 | 変更後 |
| --- | --- | --- |
| 対象プラットフォーム | Windows と macOS | macOS のみ |
| ターゲットフレームワーク | net10.0-windows10.0.19041.0; net10.0-maccatalyst | net10.0-maccatalyst |

## 影響範囲

- プロジェクトのビルドターゲットから Windows を除外する。
- Windows 固有の音声キャプチャ実装（WASAPI loopback）は対象外となる。
- macOS 向け音声キャプチャ（ScreenCaptureKit 等）のみを実装・サポートする。

## 非機能要件への影響

- Windows 向け配布・パッケージングは不要となる。
- macOS 向けの配布（framework-dependent, --self-contained false）方針は維持する。
