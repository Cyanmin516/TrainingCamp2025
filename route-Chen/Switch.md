# Switch 

## basic setting
0. switch basic including password, hostname, service, misc
- login console, ssh, security setting
- ftp upload/download startup-config, vlan, iso.bin
1. vlan ip, default-gateway
2. vlan id, name, native, voice 
4. trunk, native vlan, nonegotiate, encapsulation(route port vlan), 
a. trunk mode dynamic, auto, des
5. access, vlan id
6. layer3 port, no switchport
7. LACP and root switch
8. channel-group, port-channel
9. **port-security**
10. dhcp snooping
11. arp inspection
12. PortFast and BPDU guard
13. 故障排查

### 0 Switch basic commands
```
switch> enable
switch# configure terminal
  更改主機名稱
switch(config)# hostname S1
  建立特權密碼
S1(config)# enable password class
  進入console連線介面
S1(config)#line console 0
S1(config-line)# password cisco
  登入認證
S1(config-line)# login
S1(config-line)# exit
  更改特權密碼, 加密比明文密碼更優先使用
S1(config)# enable secret ciscociso

列出檔案
S1# show running-config

```

### 1 sample IP
```


```

### 2 sample vlan name
```


```

### 3 sample
```


```

### 4 sample trunk
```
S1(config)# interface fa0/1
S1(config-if)# switchport mode trunk
S1(config-if)# no shut
S1(config-if)# exit
S1(config)# interface fa0/5
S1(config-if)# switchport mode trunk
S1(config-if)# no shut
S1(config-if)# end
```

### 5 sample
```


```

### 6 sample
```


```

### 7 sample
```


```

### 8 sample LACP and root switch
建立聚合鏈路, 實體分離, 邏輯共用
LACP status
ON-ON : 強制使用但不溝通
active-active/passive : 主動
passive-active : 被動
```
S1(config)# interface range FastEthernet 0/1 - 2
S1(config-if-range)# channel-group 1 mode active
Creating a port-channel interface Port-channel 1
S1(config-if-range)# exit
S1(config)# interface port-channel 1
S1(config-if)# switchport mode trunk
S1(config-if)# switchport trunk allowed vlan 1,2,20
S1(config-if)# no shutdown

調整樹節點權重
S1(config)# spanning-tree vlan 1 root primary
或成為根交換器
S1(config)# spanning-tree vlan 1 priority 24576

S1#show etherchannel summary
S1#show spanning-tree active
```

### sample port-security
**#必考**
**在介面中設定安全連線埠**
```
interface fa0/5
必須要是 ACCESS模式下才能使用的規則
switchport mode access
啟用安全埠
switchport port-security
設定最大連線數量
switchport port-security maximum 3
靜態設定
switchport port-security mac-address aaa.bbbb.1234
動態學習
switchport port-security mac-address stich
```

**驗證**
```
所有security port
show port-security
指定介面詳細
show port-security interface fa0/5
學習到的MAC addresses
show port-securtiy address
```


### 10. sample  dhcp snooping
交換器DHCP偵聽
**switch dhcp snooping**
```
S1(config)# ip dhcp snooping
信任介面
S1(config)# interface f0/1
S1(config-if)# ip dhcp snooping trust
S1(config-if)# exit
管控
S1(config)# interface range f0/5 - 24
S1(config-if-range)# ip dhcp snooping limit rate 6
S1(config-if-range)# exit
啟用vlan
S1(config)# ip dhcp snooping vlan 5,10,50-52
S1(config)# end
```
**檢查**
```
查看詳細介面
show ip dhcp snooping
IP 對照 MAC
show ip dhcp snooping binding
```

### 11. sample ARP inspection
啟用DAI服務需要同時啟動dhcp snooping
檢查目的MAC, 來源MAC, 與IP地址

```
先啟用監聽DHCP然後監聽ARP
S1(config)# ip dhcp snooping
選定vlan
S1(config)# ip dhcp snooping vlan 10
S1(config)# ip arp inspection vlan 10
S1(config)# interface fa0/24
S1(config-if)# ip dhcp snooping trust
S1(config-if)# ip arp inspection trust

監控ARP 來源目的與位址需同時設定
S1(config)# ip arp inspection validate src-mac dst-mac ip
S1(config)# do show run | include validate
```

### 12. sample fastport and BPDU guard
- PortFast : 正常學習要50秒，從封鎖轉轉送。應只設定在終端裝置
- BPDU Guard : 立刻禁用錯誤介面收到BPDU。應只設定在終端裝置
- 同時設定且介面必須是access mode

**介面設定**
```
S1(config)#interface range fa0/1 - 24
S1(config-if-range)#switchport mode access
S1(config-if-range)#exit
S1(config)#exit 

開啟規則
S1(config)#spanning-tree portfast default
開啟監控
S1(config)#spanning-tree portfast bpduguard default
S1(config)# exit
```
**檢查**
```
show spanning-tree summary
```
### 13. sample 故障排查
使用 `show interfaces trunk` 的主要目的
查看 Native VLAN 設定（To view the native VLAN）

這個命令會顯示：

哪些介面是 trunk port

每個 trunk port 的 native VLAN

允許通過 trunk 的 VLAN 範圍

Trunk 模式（例如 IEEE 802.1Q）

這對於排除 VLAN 傳輸問題、確認 trunk 配置是否一致非常重要。

錯誤或不完全正確的選項解析
To examine DTP negotiation as it occurs DTP（Dynamic Trunking Protocol）相關資訊需使用 `show dtp interface` 命令，並非 show interfaces trunk。

To verify port association with a particular VLAN 若要查看某個 port 屬於哪個 VLAN，應使用 `show vlan brief` 或 show interfaces switchport。

To display an IP address for any existing VLAN VLAN 本身的 IP 設定通常透過 `show ip interface brief` 或 show running-config 來查看，與 trunk 無直接關係。



# Vlan
VLAN 透過 Trunk 技術，完美地實現了「**邏輯隔離**」與「**實體線路共用**」這兩個看似矛盾的目標。這也是 VLAN 在現代企業網路中如此重要的原因。 
第三層交換器，在單純環境下(全部線路都乙太網路)能夠安裝加速晶片，更能處理大量跨網段封包。

## define
VLANs are created at Layer 2 to reducing or eliminate **broadcast** traffic.
802.1Q file defined the switch rule
- **Type** - A 2-byte value called the tag protocol ID (TPID) value. For Ethernet, it is set to hexadecimal 0x8100.
- **User priority** - A 3-bit value that supports level or service implementation.
- **Canonical Format Identifier (CFI)** - A 1-bit identifier that enables Token Ring frames to be carried across Ethernet links.
- **VLAN ID (VID)** - A 12-bit VLAN identification number that supports up to 4096 VLAN IDs.

![Vlan](vlan.png)

## Switch vlan
預設五個vlan ID
- `vlan 1` default 所有的實體port設為1
- `vlan 1002-1005` Cisco保留的通訊
- `vlan 1006-4094` 延伸範圍，為全球企業註冊使用
- Cisco設備 工作端 Port 只對應一個 vlan 編號
- 對終端port綁定Vlan限制存取無法ping到同網段其他設備
- 不同廠牌trunk協商不穩定，手動設定為佳
- 將預設 VLAN 從 VLAN 1 更改為 VLAN 99，是為了將重要的網路管理流量與使用者流量隔離，並增加攻擊者發動攻擊的難度，從而提升整個網路的安全性。
- **安全性**，當未標記封包經過一台預設vlan99的交換機，封包會自動標記，導致無法正常接收。所以交換機之間會有CDP溝通native trunk.


## switch setting
建立拓樸
- 裝置, 介面、IP、MASK、交換埠、VLAN
- vlan id, name, ports, access
- other Vlan id ,name , ports ,trunk
- Native vlan, VOICE vlan, ip default-gateway


## 實務
1. login password (console 0, vty 0 15)
2. base configuration (hostname, banner, ip gateway, encrypt pwd)
3. vlan name
- IP (interface vlan 1 Native)
- port bind vlan access, voice
- port bind vlan trunk native vlan, allow vlan
4. trunk (nonegotiate 減少思科設備DTP協定流量)

### 常用輔助指令
```console
# show ip interface
# show vlan brief
# show interface trunk
# show interface status
(config)# interface range g0/1-2,f0/3
```

### sample
```console
SWC(config)# interface f0/4
SWC(config-if)# switchport voice vlan 40
SWC(config-if)# mls qos trust cos
SWC(config-if)# switchport mode access 
SWC(config-if)# switchport access vlan 10

SWA(config)# interface g0/1
SWA(config-if)# switchport mode trunk 
SWA(config-if)# switchport nonegotiate 
SWA(config-if)# switchport trunk native vlan 100
```

## switch Access and Trunk
交換機的 VLAN 連接模式概要
-	**Access Mode**：
	-	每個 Access Port 只能屬於一個 VLAN。
	-	進入的無標籤封包會被打上該 VLAN 的標籤 (VID)。
	-	傳出的封包會移除 VLAN 標籤，送給終端裝置。

-	**Trunk Mode**：
	-	可同時傳輸多個 VLAN 的封包。
	-	已有 VLAN 標籤的封包會檢查 VID 再轉發。
	-	無標籤封包會被套用 Native VLAN 標籤。

## Native 

會出現兩條 Trunk 但 Native VLAN ID 不同的需求，通常是這幾種情境：
1.	**跨網段的管理或隔離需求**
    -	有些網管會把不同交換機之間的管理介面放在不同 VLAN，例如一組設備的管理用 VLAN 10，另一組用 VLAN 20。
    -	雖然兩條 Trunk 都承載多個 VLAN，但因為管理端口的 Native VLAN 不一樣，便於設備在沒有 VLAN 標籤的情況下，也能正確進入對應的管理網段。
2.	**降低 Native VLAN 衝突風險**
    -	若 Trunk 連到不同廠商或不同部門的交換機，Native VLAN ID 不一致可以避免「未標籤封包」誤進錯的 VLAN。
    -	這常見於兩個網路區塊交界的連接線，讓未標籤封包不會自動混到同一 VLAN。
3.	**漸進式 VLAN 重構或遷移**
    -	網路在改版時，部分 Trunk 先改成新的 Native VLAN，而舊系統還用舊的 Native VLAN。
    -	這樣可以逐步遷移，不會一次影響全部連線。
4.	**應對不同用途的鏈路**
    -	例如一條 Trunk 專門服務語音 VLAN（Native 設為語音 VLAN），另一條專門服務監控 VLAN。
    -	這樣可以讓某些特定未標籤流量自動進入正確用途的 VLAN，而不用在端口端手動打標籤。

