能做到訊框格式轉換，作為網路邊界的中介傳遞封包

## basic
- port, ip, ipv6
- ssh remote
- port[vlan id], encapsulation dot1Q [vlan id]
- dhcp
- 備援
- 路由表 優先權
- OSPF


## switch and router command security
明文密碼在running-config
enable password [urpassword]
加密密碼且優先度高
enable secret [urpassword]


- **basic port name**: hardware order/hardware lavel/port number

| interface | shorts |
|:-|:-|
| FastEthernet 0/0 | f0/0 |
| GigabitEthernet 0/0 | g0/0 |
| Serial port 0/0/0  | s0/0/0 |
| Console port link | line console 0 |
| Remote virtual teletype | line vty 0 15 |
| port[vlan 10] | g0/0.10 |

## ip setting
1. port with ip4, one to one
```console
進入介面
R(config)# interface g0/0
設定IP
R(config-if)# ip address 192.168.0.1 255.255.255.0
啟用介面
R(config-if)# no shutdown
當衝突發生想停用IP
R(config-if)# no ip address
```
2. port with ipv6, one to multi ipv6
```console
啟動服務
R(config)# ipv6 unicast-routing
R(config)# interface g0/0
設定unicast
R(config-if)# ipv6 address 2001:db8:acad:1::1/64
設定LLA
R(config-if)# ipv6 address fe80::1/64 Link-local
R(config-if)# no shutdown
```



## 查看介面
```console
R1# show ip route
show ip interface brief
show interfaces
show interfaces trunk
show ip route | begin Gateway
| include
| section
| begin
| 
若在其他介面地下可加入前綴do
do show ...

```


## basic setting
1. vlan ip
2. vlan id, name, native, voice
3. ip default-gateway
4. trunk, native vlan, nonegotiate, encapsulation
5. access, vlan id
6. layer3 port, no switchport
7. OSPF
8. channel-group, port-channel

### 1 sample
```


```



## DHCP
發放IP給設備，或是中繼DHCP服務
### sample router
1. 訂定IP池
```console
先排除固定使用IP
R1(config)#ip dhcp excluded-address 192.168.11.1 192.168.11.9
R1(config)#ip dhcp excluded-address 192.168.11.254

設定IP池
R1(config)#ip dhcp pool LAN-POOL-2
R1(dhcp-config)#network 192.168.11.0 255.255.255.0
R1(dhcp-config)#default-router 192.168.11.1
R1(dhcp-config)#dns-server 192.168.11.6
R1(dhcp-config)#domain-name example.com
R1(dhcp-config)#end
```

2. 啟用關閉服務
```
R1(config)# no service dhcp
R1(config)# service dhcp
R1(config)#
```

3. 路由器中繼DHCP
```
中繼router R2
R2(config)# interface g0/1
R2(config-if)# ip address dhcp
R2(config-if)# no shutdown

中繼router後面的router R1
IP helper指定到對接port而非DNS可以減少路由表轉換，效能最好
R1(config)# interface g0/0/0
R1(config-if)# ip helper-address 192.168.11.6
```

4. port with vlan on-side-route
```
R1(config)# interface G0/0/1.10
R1(config-subif)# description Default Gateway for VLAN 10
R1(config-subif)# encapsulation dot1Q 10
R1(config-subif)# ip add 192.168.10.1 255.255.255.0
R1(config-subif)# exit
R1(config)#
R1(config)# interface G0/0/1.20
R1(config-subif)# description Default Gateway for VLAN 20
R1(config-subif)# encapsulation dot1Q 20
R1(config-subif)# ip add 192.168.20.1 255.255.255.0
R1(config-subif)# exit
R1(config)#
R1(config)# interface G0/0/1.99
R1(config-subif)# description Default Gateway for VLAN 99
R1(config-subif)# encapsulation dot1Q 99
R1(config-subif)# ip add 192.168.99.1 255.255.255.0
R1(config-subif)# exit
R1(config)#
R1(config)# interface G0/0/1
R1(config-if)# description Trunk link to S1
R1(config-if)# no shut
R1(config-if)# end
R1#
*Sep 15 19:08:47.015: %LINK-3-UPDOWN: Interface GigabitEthernet0/0/1, changed state to down
*Sep 15 19:08:50.071: %LINK-3-UPDOWN: Interface GigabitEthernet0/0/1, changed state to up
*Sep 15 19:08:51.071: %LINEPROTO-5-UPDOWN: Line protocol on Interface GigabitEthernet0/0/1, changed state to up

檢查
show ip route
show ip interface brief
show interfaces
show interfaces trunk
```


## HSRP
路由器備援
Router設定
```
R1(config)#interface g0/1
R1(config-if)#standby version 2
R1(config-if)#standby 1 ip  192.168.1.254
R1(config-if)#standby 1 priority 150
R1(config-if)#standby 1 preempt 
R1(config-if)#end

檢查
R1#show standby 
R1#show standby brief
```


### 管理距離 Administrative Distance
路由表優先權，數值小優先

| 協定 | 優先權| 
|:-|:-|
| 內部EIGRP | 90 |
| OSPF | 110 |
| RIP | 120 |
| 直連 | 0 |
| 手動設定 | 1 |

**靜態路由**
```bash
ipv6 unicast-routing
ipv6 route 2001:db8:acad:8::/64  2001:db8:acad:8::1

ip route 172.16.1.0 255.255.255.0 GigabitEthernet 0/0/0
不會有度量值
不顯示靜態路由

ipv6 route 2001:db8:acad:1::/64 s0/1/0
S   2001:DB8:ACAD:1::/64 [1/0]
     via Serial0/1/0, directly connected
ip route <ip> <mask> <interface> <next ip>
ipv6 route <ip/len> <interface> <link-local>
```

**預設路由**
```bash
ip route 0.0.0.0 0.0.0.0 { ip | interface }
每個IPv6一定要綁定介面所以不用設定
ipv6 rout ::/0 { ip }
```

**浮動靜態路由** : 更改度量值，數值小優先，讓備援通道接續工作
[AD協定方式/度量值]
```sh
R(config)# 
ip route 0.0.0.0 0.0.0.0 172.16.2.2 2
ip route 0.0.0.0 0.0.0.0 10.10.10.2 5
ipv6 route ::/0 2001:DB8:CAFE:2::1
ipv6 route ::/0 2001:DB8:breed:3::10 5
R#
show ip route
show ipv6 route
```
路由表僅顯示最優先介面IP,備援的隱藏

對照順序
1. 只看目的地IP/network
2. 遮罩**最長**
3. AD值**最小**
4. 度量值**最小**


## 設定OSPF
```console
R(config)# router ospf 10
R(config-router)# router-id 1.1.1.1

do show ip route
C 10.1.1.4/30
L 10.1.1.6/32

指定網段
R1(config-router)# netwrok 10.1.1.4 0.0.0.3 area 0
指定IP
R2(config-router)# network 10.1.1.6 0.0.0.0 area 0
指定介面
R3(config)# interface g0/0/0
R3(config-if)# ip ospf 10 area 0
關閉末端的OSPF
R(config-router)# no passive-interface g0/0/0
show ip protocols
show ip route
O IP/mask [110/128] ...

show ip ospf interface g0/0/0
```


### OSPF 優先順序設定
優先序(0-255)選最高的，預設1，相同優先時選IP最高者
重新設定DR
```bash
show ip ospf interface g0/0/0
show ip ospf neighbor 

R1(config)# interface G0/0/0
R1(config-if)# ip ospf priority 255

R2(config)# interface G0/0/0
R2(config-if)# ip ospf priority 0

R3(config)# interface G0/0/0
R3(config-if)# ip ospf priority 100

R1# clear ip ospf process
[no]:y
R2# clear ip ospf process
[no]:y
R3# clear ip ospf process
[no]:y
```


### 設定OSPF COST度量值
```
Router(config-router)# auto-cost reference-bandwidth <defautl_100_Mbps>
R1(config)# interface g0/0/1
R1(config-if)# ip ospf cost 30
R1(config-if)# interface lo0
R1(config-if)# ip ospf cost 10
R1# show ip route ospf
```

### 2.4.7 Hello封包間隔
乙太網路 : hello 10s, dead 40s
鄰居之間要有相同的hello參數
修改hello封包間隔, dead = time x 4
```
R1(config)# interface g0/0/0
R1(config-if)# ip ospf hello-interval 5
R1(config-if)# ip ospf dead-interval 20

modify immediatety
```

### 2.5 OSPFv2(IPv4)傳播預設靜態路由
邊際路由設定
1. 手動設定靜態預設路由
2. 透過OSPF宣告

```
R2(config)# ip route 0.0.0.0 0.0.0.0 s0.0.0
R2(config)# router ospf 1
R2(config-router)# default-information originate

檢查
R2# show ip route 
S*   0.0.0.0/0 is directly connected, Serial0/1/0
R1# show ip route 
O*E2 0.0.0.0/0 [110/1] via 172.16.3.2, 00:01:46, Serial0/0/0
```

### 2.6 驗證OSPF
- 網段不同大小
- Hello 間隔不同
- 錯誤設定成被動模式
- 網路類型不匹配 broadcast,point-to-point.

```
show ip interface brief
show ip route
show ip route ospf
show ip ospf neighbor
show ip protocols
show ip ospf interface brief
show ip ospf interface g0/0/0
```


## ACL 語法
```sh
設定 ACL 規則
Router(config)# access-list access-list-number {deny | permit | remark text} source [source-wildcard] [log]
接受或拒絕，remark用來記錄訊息方便維護沒有功能

設定介面
Router(config-if) # ip access-group {access-list-number | access-list-name} {in | out}

do show access-list
show ip interface s0/0/0 | include access list

介面設定ACL規則
interface g0/0
ip access-group 20 in                                                        


ip access standard LAN2-FILTER
R(cdf-std-nacl)# ip access-group LAN2-FILTER out         

show running-config
show access-lists

```

### 5.3.4保護連接埠
```sh
設定帳戶
R1(config)#username ADMIN secret class
設定原則
R1(config)#ip access-list standard ADMIN-HOST
R1(config-std-nacl)#permit host 192.168.10.10
R1(config-std-nacl)#deny any
R1(config-std-nacl)#exit
設定遠端
R1(config)#line vty 0 4
R1(config-line)#login local
R1(config-line)#transport input telnet
設定遠端存取源則
R1(config-line)#access-class ADMIN-HOST in
檢查
R1#show access-lists
```


