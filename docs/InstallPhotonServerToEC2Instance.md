# EC2インスタンスでのPhotonServerインストール

ハマった箇所のまとめ

## SDKダウンロード時にログインできない
IEのインターネット設定→セキュリティタブより下記の設定を行う。  
※EC2のWindowsServerだとIEを使わざるを得ない。  
　尚、IEの用語については全く覚えていないので悪しからず。  

1. http://*.photonengine.com/ をホワイトリストに突っ込む
2. グローバル設定のスクリプト実行関係を全部Enabledにする

- Photonのサイトをホワイトリストに突っ込むのは、ログイン時に「Invalid Parameter」と怒られるのを防ぐため
- グローバル設定のスクリプト実行関係を全部Enabledにするのは、Captchaを表示させるため

## セキュリティグループでポート開けてるのに繋がらない

セキュリティグループでポートを開けるだけでは駄目。  
WindowsServer内のファイアウォールでもポートに風穴を開ける必要がある。  
詳しくは[ここ](https://rainbow-engine.com/winserver-firewall-port-allow/)を参照。  
