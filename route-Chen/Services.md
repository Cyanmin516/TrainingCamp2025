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


設定 **SNMP Trap Receiver** (SNMP 陷阱接收器) 是為了讓你的網路管理系統能夠**主動接收**來自網路設備的**即時告警和狀態變更通知**。
s
簡單來說，SNMP Trap 的概念就像是網路設備在遇到重要事件時，主動「打電話」通知網管系統，而不是等著網管系統定時去「詢問」牠們的狀態。

為什麼需要設定 Trap Receiver？主要有以下幾個原因：

1.  **即時告警與事件通知：**
    * **主動性：** 設備在發生異常（如硬體故障、連接埠斷開、CPU 使用率過高、溫度異常、風扇停轉等）時，會立即發送一個 Trap 訊息。
    * **速度：** 這比定時輪詢 (Polling) 更快。輪詢是網管系統主動去詢問設備狀態，如果設備在輪詢之間發生問題，可能會被錯過。Trap 則是設備自己通知，確保你能在第一時間得知問題。
    * **減少延遲：** 對於需要快速反應的事件（如網路鏈路中斷），Trap 能夠顯著縮短故障發現的時間，從而更快地進行故障排除，減少網路停機時間。

2.  **效率與資源節省：**
    * **按需通知：** 設備只在發生重要事件時才發送 Trap，而不是持續不斷地發送大量數據。這降低了網路的負載和設備的處理壓力，也減少了網管系統需要處理的數據量。
    * **對比輪詢：** 如果沒有 Trap，網管系統可能需要非常頻繁地輪詢設備，才能及時發現問題，這會消耗更多的網路帶寬和設備資源。

3.  **獲取詳細的事件資訊：**
    * **上下文資訊：** SNMP Trap 訊息通常包含豐富的上下文資訊，例如事件的具體類型（透過 OID - Object Identifier）、發生時間、相關的設備資訊、錯誤代碼，甚至是額外的變數綁定 (Variable Bindings)，這有助於快速診斷問題的根源。

4.  **監控的完整性：**
    * **填補輪詢的盲點：** 某些特定類型的事件，如硬體電池故障或開關面板問題，可能不容易透過常規的輪詢來偵測，但可以透過 Trap 來獲取。
    * **全面監控：** 結合輪詢和 Trap，可以實現更全面、更可靠的網路設備監控。輪詢負責定期檢查設備的整體狀態和性能指標，而 Trap 則負責處理突發性的、關鍵的事件通知。

**總結來說，設定 SNMP Trap Receiver 是為了建立一個更智慧、更主動、更即時的網路監控機制。它讓你能夠從被動等待輪詢結果，轉變為主動接收來自設備的關鍵告警，從而更有效地管理和維護網路的穩定運行。**