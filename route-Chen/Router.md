能做到訊框格式轉換，作為網路邊界的中介傳遞封包

## basic
- port, ip, ipv6
- port[vlan id], encapsulation dot1Q [vlan id]
- 

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