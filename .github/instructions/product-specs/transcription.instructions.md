---
applyTo: "src/**/Services/Transcription/**,src/**/Services/Diarization/**"
---

## 文字起こし実装方針（F2）

- キャプチャした音声をリアルタイムにテキストへ変換すること。
- STT エンジンは Azure Speech Service を使用すること。
- 認証は DefaultAzureCredential（InteractiveBrowserCredential のみ有効）を使用し、API キー認証は使用しないこと。
- 対応言語は英語を主対象とし、多言語対応を視野に入れた設計にすること。
- 書き起こし結果はタイムスタンプ付きで保存すること。

## 話者識別実装方針（F5）

- 音声ストリーム内の話者を識別し、匿名ラベル（Speaker A, Speaker B, …）を割り当てること。
- 話者識別エンジンは Azure Speech Service の Speaker Diarization 機能を使用すること。
- 同一セッション内で一貫したラベルを維持すること。
- 話者の実名は扱わず、匿名性を保つこと。
