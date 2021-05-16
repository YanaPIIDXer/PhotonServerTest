# PhotonServerTest
PhotonServerの実験用リポジトリ

## Event / Operation

Photonの通信には大きくこの２種類が存在する。  
- Event
  - サーバから一方的に通知される通信
  - クライアント側から投げる事は出来ない
- Operation
  - クライアントがサーバに通信を投げて、サーバはその結果を返す
    - 要はRequest / Response
    - ただ、例えばプレイヤーの移動をサーバに投げた所でサーバは「何を返せというのか」という話になるので、必ずしもRequestに対してResponseがあるとは限らない。

Eventはサーバ主体の通信で、Operationはクライアント主体の通信。  
