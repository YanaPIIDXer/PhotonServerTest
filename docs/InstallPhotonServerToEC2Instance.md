# EC2インスタンスでのPhotonServerインストール

ハマった箇所のまとめ

## SDKダウンロード時にログインできない
EC2のWindowsServerだとIEを使わざるを得ない。  

1. http://*.photonengine.com/ をホワイトリストに突っ込む
 - これがないとログイン時に「Invalid Parameter」と怒られる
2. グローバル設定のスクリプト実行関係を全部Enabledにする
 - これをやらないとCaptchaが動作せず、ログインする事ができない
