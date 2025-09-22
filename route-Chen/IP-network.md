# IP
host/mask define a IP
IPv4 用盡了
IPv6 支援更大架構

雙協定並行

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



# Notes of IPv6
- 目次
- 傳播方法
- 名詞區分
- DHCP運作

#IPv6 #GUA #LLA #SLAAC #DHCPv6 #multicast
## 1. 傳播方法
* unicast - 外部IP
* multicast - 群播、多播
* anycast - ask nearest device with unicast addresses.

## 2. 名詞區分
GUA and  LLA. GUA is global, and LLA is local.
* **GUA:** 
    * 定義: IPv6 `global unicast addresses` (GUAs) are globally unique and routable on the IPv6 internet.
    * 實務: Currently, only GUAs with the first three bits of 001 or `2000::/3` are being assigned, as shown in the figure. end 3fff::/3
* **LLA:** 
    * 定義: An IPv6 link-local address (LLA) enables a device to communicate with other IPv6-enabled devices on the same link and only on that link (subnet).
    * 實務: IPv6 LLAs are in the `fe80::/10` range. The /10 indicates that the first 10 bits are 1111 1110 10xx xxxx. The first hextet has a range of 1111 1110 1000 0000 (fe80) to 1111 1110 1011 1111 (febf). end febf::/10
* **self-ping**
    * 定義:selft test ip `::1/128`
    
## 3. Router and DHCP
Client sent RS to DHCPv6 and recieve RA.
* **RA** : The ICMPv6 RA message includes the following:
    * a. **Network prefix and prefix length** - This tells the device which network it belongs to.
    * b. **Default gateway address** - This is an IPv6 LLA, the source IPv6 address of the RA message.
    * c. **DNS addresses and domain name** - These are the addresses of DNS servers and a domain name.

* **SLAAC method** : There are three methods for RA messages:
    * **Method 1: SLAAC** - RA from router and create own interface ID.
    * **Method 2: SLAAC with a stateless DHCPv6 server** - RA from router and ask DHCPv6 where DNS. 
    * **Method 3: Stateful DHCPv6 (no SLAAC)** - gateway from router and ask DHCPv6 for IP. 

### IPv6 Address Configuration Methods Summary
| Configuration Method | **Router RA (Router Advertisement)** | **DHCPv6 messages** |
| :----  | :----  | :---- |
| **1. SLAAC** | Network Prefix, Prefix Length, Default Gateway (Router's Link-Local Address)               | **None** (DHCPv6 not used for any configuration)                                                  |
| **2. SLAAC + Stateless DHCPv6** | Network Prefix, Prefix Length, Default Gateway (Router's Link-Local Address) | DNS Server Addresses (and other "stateless" options)             |
| **3. Stateful DHCPv6 (no SLAAC)** | Default Gateway (Router's Link-Local Address) | Assigned IPv6 Address, DNS Server Addresses (and all other options) |

## multicast
`ff02::1` All-nodes multicast group
`ff02::2` All-routers multicast group