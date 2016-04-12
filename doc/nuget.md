# NuGet

## 概要

MyGetサーバで管理する必要がある
github.io/aspnet/repoの中ではdev用とmaster用の2種類が用意されてあり、バージョン管理されている。

## サーバのアカウント作成

Nightly Build

## クライアントサイドの仕組み
https://docs.nuget.org/consume/nuget-config-file#nuget-config-extensibility-point  
NuGet.configは親フォルダに向かって探査する。  
デフォルトのNuGet.Configファイルは一番最初に探査されるが、上書きで探査する。  
dnu restoreの仕様ではなくて、NuGet.exeの仕様  
