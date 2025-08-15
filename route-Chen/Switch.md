# Switch 

## basic setting
0. switch basic including password, hostname, service, misc
- login console, ssh, security setting
- ftp upload/download startup-config, vlan, iso.bin
1. vlan ip, default-gateway
2. vlan id, name, native, voice 
4. trunk, native vlan, nonegotiate, encapsulation
5. access, vlan id
6. layer3 port, no switchport
7. OSPF
8. channel-group, port-channel

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

### 4 sample
```


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


