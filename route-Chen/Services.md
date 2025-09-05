# what do we use services
企業及網路管理服務

0. setting order
1. 萬用型路由設定
2. WLC 集中控制AP(LAP)
3. SNMP AND RADIUS 
4. 統一管理 router, WLC, DHCP, SNMP RADIUS 設定

## QUERY
1. route: ip, mask, gateway
2. DHCP
3. WLC
4. SNMP AND RADIUS

## sample 1
[LAN] 本地直連
1. LOGIN
ip: 192.168.0.1
user/password: admin/admin
2. ip, mask, gateway
3. WPA2 PERSONAL 共享密鑰
4. WLAN故障排除



## SAMPLE 2
- WPA+WPA2
- WPA2 PSK(public password key):password
- flex content
    - local switch
    - local auth



## SAMPLE 3 SNMP AND RADIUS
- WPA2 POLICY
- AES
- 801.X
- flex content
    - local switch
    - local auth

**controller**
- DHCP scope: WLC IP

**MANAGEEMENT**
- SNMP 
    - trap reciever
        - community name: 
        - ip
        - status

*CONNECTION*
- NAME
- SSID
- WPA2 enterprise
 

## sample router, WLC, DHCP, SNMP RADIUS 設定
0. login
1. controller
- interface :  
    - VLAN NAME,VLAN ID 
    - PORT, IP, MASK, GATEWAY, DHCP 
- Interal DHCP server
    - pool, gateway, dns
2. SECURITY (WLC)
- AAA, RADIUS, Authentication
    - server Index, IP, password, port [1812]
3. MANAGEMENT
- SNMP
    - **trap reciever** 
        - NAME, IP, enable
4. WLANs
本地認證
- General: NAME, SSID, status, interface
- security: WPA+WPA2, WPA2 policy, AES,PSK, password
- advanced: flex content switch and auth

伺服器認證
- General: - General: NAME, SSID, status, interface
- security: WPA+WPA2, WPA2 policy, AES,802.1X
- AAA : interface
- advanced: flex content switch and auth

5. user connect
本地認證: 設定SSID,WPA2 personal, password
伺服器認證: 設定SSID, WPA2 ENTERPRISE, username, password


## gemini
簡單來說，設定的邏輯流程是：

1. 建立網路基礎： 先設定好 Router，確保 IP 位址、路由和網路連通性。
2. 自動分配 IP： 設定 DHCP Server，讓設備能自動取得 IP。
3. 核心網路服務： 設定 WLC 以管理無線網路，並將其設定連接到 RADIUS Server。
4. 安全驗證： 設定 RADIUS Server，使其能為 WLC 提供的無線網路進行使用者驗證。
5. 監控與管理： 設定 SNMP，讓網路管理員可以監控 Router 和 WLC 的運行狀態。