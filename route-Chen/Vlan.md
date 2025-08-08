# Vlan
VLAN 透過 Trunk 技術，完美地實現了「**邏輯隔離**」與「**實體線路共用**」這兩個看似矛盾的目標。這也是 VLAN 在現代企業網路中如此重要的原因。
why use vlan? Control Access with QoS and Security. `

## define
VLANs are created at Layer 2 to reducing or eliminate **broadcast** traffic.
802.1Q file defined the switch rule

- **Type** - A 2-byte value called the tag protocol ID (TPID) value. For Ethernet, it is set to hexadecimal 0x8100.
- **User priority** - A 3-bit value that supports level or service implementation.
- **Canonical Format Identifier (CFI)** - A 1-bit identifier that enables Token Ring frames to be carried across Ethernet links.
- **VLAN ID (VID)** - A 12-bit VLAN identification number that supports up to 4096 VLAN IDs.

![Vlan](vlan.png)
## Switch vlan
預設五個
- `vlan 1` default 所有的實體port設為1
- `vlan 1002-1005` Cisco保留的通訊
- `vlan 1006-4094` 延伸範圍，為全球企業註冊使用
- Cisco設備 工作端 Port 只對應一個 vlan 編號
- 對終端port綁定Vlan限制存取無法ping到同網段其他設備
- 不同廠牌trunk協商不穩定，手動設定為佳
- 將預設 VLAN 從 VLAN 1 更改為 VLAN 99，是為了將重要的網路管理流量與使用者流量隔離，並增加攻擊者發動攻擊的難度，從而提升整個網路的安全性。
- **安全性**，當未標記封包經過一台預設vlan99的交換機，封包會自動標記，導致無法正常接收。所以交換機之間會有CDP溝通native trunk.


## switch setting
1. login password (console 0, vty 0 15)
2. base configuration (hostname, banner, ip gateway, encrypt pwd)
3. vlan name
IP (interface vlan 1 Native)
port bind vlan access, voice
port bind vlan trunk native vlan, allow vlan
4. trunk (nonegotiate 減少思科設備DTP協定流量)


## 實務
建立拓樸
裝置, 介面、IP、MASK、交換埠、VLAN
vlan id, name, ports, access
other Vlan id ,name , ports ,trunk
Native vlan, VOICE vlan, ip default-gateway

### 常用輔助指令
show ip interface
show vlan brief
show interface trunk
show interface status
interface range g0/1-2,f0/3


### sample
SWC(config)#interface f0/4
SWC(config-if)#switchport voice vlan 40
SWC(config-if)#mls qos trust cos
SWC(config-if)#switchport mode access 
SWC(config-if)#switchport access vlan 10

SWA(config)#interface g0/1
SWA(config-if)#switchport mode trunk 
SWA(config-if)#switchport nonegotiate 
SWA(config-if)#switchport trunk native vlan 100
