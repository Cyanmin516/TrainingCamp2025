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

