能做到訊框格式轉換，作為網路邊界的中介傳遞封包

## basic
- port, ip, ipv6
- ssh remote
- port[vlan id], encapsulation dot1Q [vlan id]
- dhcp
- 備援

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
R1#show standby 
R1#show standby brief
```







