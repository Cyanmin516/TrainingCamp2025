# Experience

Some skills and knowledges are from teacher's experience.


- **kiss rule** : keep it simple stupid.
- **Ctrl+Shift+6** : stop cmd in Cisco devices
- **tab** : autocompletion in most OS
- 當網管的必要能力 : 建立文件`實體拓樸圖`
- Windows 查看路由表 : `netstat -r`
- 路由器CLI ping : 失敗為`.` 成功為`!`
- IPv6位址EUI-64 : 很少亂數產生，能追蹤實體序號
- IPv6移除不使用IP : 網卡可以接受多個IP同介面
- 固定 IP 讓我們能夠精確地定位和管理網路中的「**靜態硬體資源**」。
- VLAN 讓我們能夠靈活地基於「人員、部門或功能」對網路進行**邏輯隔離**和**策略控制**。

#資安
- 不同頻率備份，日誌每天、設定每周、系統每季
- 驗證備份程序
- 異地備份，至少三十公里實體距離
- 預設帳號密必須改
- 單人作業不要紀錄實體密碼(工作唯一性)

#思科資安
- auto secure
- 密碼試錯
- login time-out

#實務
1. 戶外事件，落雷感電燒壞switch
2. 廠商保固，HP有限終身保固，偏好國際品牌
3. 台灣設備，有時候重開就好了
4. 備援設備，無縫接軌中斷的服務
5. 設備生命週期，8-10年


## 小型網路成長實務
- **網路文件** 非常重要，實體與邏輯拓樸 
- **裝置清單** 網路架構的設備
- **預算** 紀錄IT預算
- **流量分析** wireshark 或更高級


#員工網路使用率
照片快照，記錄各種設備狀態
- 用遠記得要留使用紀錄，才能有比較基準
- 零信任
- 一人一帳號


#Show cisco discovery protocol (CDP)
- Cisco 設備專用協定
- Layer 2 protocol
- 可知道設備硬體資料、韌體版本
- 關閉服務， (config)# no cdp
- 關閉介面， (config-line)# no enable cdp

#實務
紀錄問題與措施
- cisco 故障回報只會傳送給設備，所以下指令`terminal monitor`，來取得設備debug資訊，並遠端檢查。
- IPv4在同一張網卡只能有一個IPv4，- 但ipv6可以在同一張網卡有多個IPv6

switch ip interface
`no switchport` change to a route port, and auto up

## 實務
職務上都是維護既有系統，所以檢查能力為必備

1. 介面卡問題
    - status up, 有電
    - protocol, 協定(速度)
    - 通常 status up and protocol down 的錯誤，兩邊都使用定速傳輸(1G/100M)，傳輸功率不同錯誤
>show ip interface brief 

2. IP設定
    - mask對IPv4很重要，(廣播位置)

3. PING
    - windows系統 預設關閉
    - arp 查看同個網域的設備
4. show option
- section
>show ip interface brief | section vty
- include
包含，但不準確，up難用
>show ip interface brief | inlcude up
>show interface| include Description|connected
- exclude 
排除，未使用，較常用
>show ip interface brief | exclude unassigned
- begin
從哪裡開始
>show ip route | begin Gateway

#practice1.5.7

5. show history
    - cisco only history 10 orders
    - 華為借鏡cisco設備
    - `terminal history size 200`
    檢測 廠商操作，常留後門(救援用)，需要記憶體
#practice1.5.9
`pipe-line` : 事件選擇或` | ` 前後選項之間包含空白
`or logic` :  邏輯選擇或`|` , 前後選擇間相連不包含空白


6. 市場 
70%的市面上路由器為Cisco系統。

7. 最後選擇路由
靜態預設路由
```sh
S* 0.0.0.0/0 [1/0] 2090.165.200.224
```


### 實務
1. 三台實體機 跑10G到交換器，擁有80台虛擬機
2. 動態平衡資源
3. 原生Vlan自己設定

登入管理是 line console|vty|aux

#vlan 依照標籤轉送, 12bit VID=4096
- `1-1005` vlan 標準通訊
- `1006-4095` 企業使用
- Cisco設備 預設一個終端PORT只綁一個vlan

VLAN 透過 Trunk 技術，完美地實現了「**邏輯隔離**」與「**實體線路共用**」這兩個看似矛盾的目標。這也是 VLAN 在現代企業網路中如此重要的原因。

1. Vlan 刪除 屬於該vlan 的port將不屬於任何port
2. 不屬於任何vlan的port 在新增vlan時自動掛上


#第三層交換器
有能力轉換訊框，可安裝加速卡

```sh
change to a route port, and auto up
Switch(config-if)# no switchport 

啟動第三層路由
Switch(config)# ip routing

更啟動IPv6一樣需要啟動
Switch(config)# ipv6 unicast-routing
```


如果你的設備較舊，或者你在使用「Router-on-a-stick」架構，你需要手動設定 switchport trunk encapsulation dot1q。

如果你的設備是近年來購買的主流型號，通常已經不需要這個指令，直接設定 switchport mode trunk 即可。


### 網路設備名詞
**支援** : 不一定能使用，可能要額外購買。
**提供** : 一定能啟用。

### 6.3.2 常見問題 
show etherchannel summary
show running-config

先停用再新增規則
interface range g0/1-2
no channel-group
channel-group 1 mode active


實體都設置為中繼連接(trunk)port-channel自動更新
interface port-channel 1
trunk

## 實務
- 防火牆在最外層，常常與DHCP服務綁在一起。
- 基本上會有兩台DHCP服務器
- DNS實務上也會架設兩台
- ip常保留最後15個 240-254 (255廣播IP),提供路由器交換器印表機等附屬設備使用
- 思科指令只有小寫，所以用大寫來辨識修自訂一名稱
- 路由器預設開啟DHCP
- 拿到設備第一個檢查所有服務，並關閉不必要的服務，提升資源使用
- DHCP前有交換器會先做STP生成樹需要正常50秒啟動(RSTP20秒)後才拿到IP
- IPV6 無狀態SLAAC最常使用
- 路由IPv6優先於IPv4
- 備援路由兩台分別對兩家外網

**驗證**
```
show running-config | section dhcp
show ip dhcp binding
show ip dhcp server statistics
```
DHCP attack
注意DHCP攻擊，假冒MAC要IP 8小時

## 10  LAN Security Concepts
NG :next generat

NAC裝置:驗證、授權、計量(AAA)
身分控管

新世代防火牆:
- 過濾URL, 阻擋惡意網址
- 過濾port, 阻擋不必要的服務FTP


### 10.3.2 攻擊
MAC表攻擊
VLAN攻擊
DHCP攻擊
ARP攻擊
位置詐騙攻擊
STP攻擊

### 10.3.3 緩解
連接埠安全
DHCP監聽 
動態ARP監測(DAI)
IP守衛

## 實務

- vlan整個系統 在100個以內
- 大部分switch 只會有10個Vlan做管理
- switch 並不會設定所有vlan
- 重要設施才會設置native vlan


### 實務
DLP裝置: data leak protection
- 關鍵字管理過濾
- 禁止含有關鍵字的檔案
內部網路防火牆隔絕病毒汙染


### 12.7.4 共用金鑰驗證
1. WEP: 靜態金鑰，已經破解
2. WPA: 更改每個封包密鑰
3. WPA2: AES加密
4. WPA3: 還在普及

個人:預先共享金鑰，WPA2 personal
企業:WPA2 Enterprise ，要指向認證伺服器

## 實務
- 家用整合路由(fat AP) 包含 AP, DHCP, 路由, 線上更新
- 企業 輕量刑AP (LAP), 30 顆以上控制

- 管理vlan 與 資料vlan要區隔


## 13.4 故障排除

1. 物理實體:電源/網路線
2. 網路設定: IP/mask
3. 干擾:藍芽、微波爐
4. 連線裝置支援: 舊設備走2.4G, 新設備走5G
5. 更新韌體


## 13 無線路由器
通常包含WLAN, DHCP, NAT, QoS


無線路由器設定
```
登入無線路由器
使用LAN PORT登入(192.168.0.1)
預設帳號密碼 admin/admin ，有風險一定要改
2.4GHz 如果舊設備加入會拖慢
5GHz 建議只使用新協議
設定SSID名稱
更改頻道，使用手機軟體掃描2.4頻道(1,6,11)三個互不干擾

QoS 預設 中，調整優先轉發順序
連接埠轉發，設定port自動轉發內部IP
```


## 實務上
任何組態都要留檔
業務上可能會出現客戶需要緊急修復需求like停電、淹水、維修。
網管一定要知道的，網路架構圖，介面，設定檔，伺服器管理帳號檔案或找到承辦人。

TWNIC:申請台灣IP、ASN(多重路由分享)

手動設定路由表時，指綁介面不指定IP會出錯

ping 不通改用tracert



#常用檢測軟體
網路wireshark
硬體CrystalDiskInfo


層級
網路架構IPv4, IPv6
交換機 vlan
路由器 無線網路, 備援線路
資訊安全更新


# 實務

交付期限原則
- 不要算剛好
- 而且不少於三分之二期限

老闆喜歡的一句話
- 我算你便宜點



# 名詞
There is congestion 網路壅塞
