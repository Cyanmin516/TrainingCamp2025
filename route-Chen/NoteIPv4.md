# IPv4
- 32 bits, 8 bit trans to a decimal, 255.255.255.255
- 獨立於每個網域 IP/mask, IP為主機位置，mask為網域大小
    - sample:host 192.168.1.1/24, 網域位置192.168.1.0, 廣播位置192.168.1.255
- 跨網段(segment)需經過路由器，在Windows 稱預設閘道(default gateway), 或是預設路由
- 

## 1 常用的保留IP
|IP|feture|
|:-|:-|
|`0.0.0.0/8`|預設路由|
|`10.0.0.0/8`|私人IP|
|`127.0.0.0/8`|本機遞迴|
|`169.254.0.0/16`|區域IP，無法取得DHCP|
|`172.16.0.0/12`|私人IP|
|`192.168.0.0/16`|私人IP|
|`224.0.0.0/4`|群播多播|
|`240.0.0.0/4`|保留不使用|
|`255.255.255.255/32`|區域廣播|

### 特殊IP
|IP|feture|
|:-|:-|
|`100.64.0.0/10`|ISP使用|
|`192.0.0.0/24`|IETF|
|`192.0.2.0/24`|網路測試|
|`192.88.99.0/24`|6to4| 
|`198.18.0.0/15`|網路測試|
|`198.51.100.0/24`|網路測試|

### sample
簡單服務發現協定（SSDP，Simple Service Discovery Protocol）
在IPv4環境，當需要使用多播方式傳送相關訊息的時候，SSDP一般使用多播位址239.255.255.250和UDP埠號1900。
>239.255.255.255:1900


## 2 指令
ping 確認網路連通，windows系統預設關閉
```
Windows 系統
Win+R > wf.msc
input rule ICMPv4 private > boot;
output rule ICMPv4 private > boot;
then ping 8.8.8.8
內網回應 <10ms 為正常
```

## 3 MAC位址與IP
16 bits，前8碼為廠商編號，後八碼為流水號
使用終端機指令
>arp -a

呈現IP與MAC映照表

### 保留
`ff-ff-ff-ff-ff-ff` 是廣播 MAC 位址
`01-00-5e-xx-xx-xx` 這些 MAC 位址是標準的多播 MAC 位址格式。任何以 01-00-5E 開頭的 MAC 位址都是用於多播流量。


## 4 通訊埠
常用通訊埠
Windows查詢 `C:\Windows\System32\drivers\etc\services`