# OPENFILERESA
- speacher: pro. Tseng
- date: 2025-9-18
- class 13

#ftp #raid

## raid
/dev/md0
md0=software volumn


raid-0 = striped, all in one
raid-1 = mirror, one file one backup
注意最小磁碟會變成最終大小
raid-5 = less mirror, at least 3 harddisc
raid-10 = use raid-1 then raid 0

partition => raid => volumn group

spare備援硬碟

## openfile
login server
everytime bootup set IP
ifconfig eth0 IP #這是暫時IP
切換到VM client win10 1909使用IE開啟
http://IP:446
警告不安全>繼續


**service**
iSCSI Target 'Enable' and 'Start'

**Volumes**
/dev/sda: 系統碟不要動

target= server

- volumn
    - Block device
        - Edit disc: `/dev/sdb/`
        設定開始與結束範圍 與前一顆間隔50+
        - partition type: physical/raid member
    - Software Raid
        - **raid 0** , 合為一
        - **raid 1**, 雙倍份且空間數量為2倍數(取最小)
        - **raid 5**, 邏輯備份(3+空間)
        - **raid 10**, 先raid 1 後raid 0
        - **spar**e 被選但不使用
    - Volumn Group
        - `/dev/md0` raid磁碟
        - `/dev/sdb1` physical磁碟切割
        - add VG
    - iSCSI Targets
        - target configuration
            - target IQN >  create 建立標籤
        - LUN Mapping 
            - if only one, auto mapping
            - R\W mode(write-thru/wirte-back/read-only)
            1. **write-thru** 立刻寫入
            2. **write-bakc** 定期寫入
            3. **read-only** 僅下載
            - transport mod
            1. **backio** 最快
            2. **filesystem** 等待檔案系統
        - Network ACL
            - change Access the option from **Deny** to **Allow**
            - **Update**

- system
    - Network Access configuration
        - NAME
        - Host
        - netmask 255.255.255.255 只限定一台
        - allow
    - **Update**


client iSCSI啟動器
目標: ip

變成網路磁碟機
裝置管理員

### NAT 技術
私人網域轉往公開網路


## Gemini
您好，很高興為您解答關於儲存裝置分區、RAID 和 Volume Group 的問題。這三個步驟通常是大型伺服器或儲存系統在配置儲存空間時的標準流程，它們各自扮演著不同的角色，共同的目的就是為了建立一個穩定、高效且易於管理的儲存環境。

---

### Hard Disk Partition (硬碟分區)

**硬碟分區**是將單一物理硬碟劃分為多個邏輯區域的過程。每個分區都可以被視為一個獨立的磁碟。這樣做的主要原因有：

* **系統與資料分離：** 您可以將作業系統安裝在一個分區（如 C 槽），而將個人資料或應用程式放在另一個分區（如 D 槽）。這樣，即使系統分區因故需要重新格式化，其他分區的資料也能被保留。
* **多作業系統：** 許多使用者會在一台電腦上安裝多個作業系統，每個作業系統都必須安裝在獨立的分區上。
* **管理便利性：** 將大容量硬碟分區可以讓檔案系統的檢查、管理和維護工作變得更容易。

簡單來說，硬碟分區是所有儲存配置的**基礎步驟**。

---

### RAID (磁碟陣列)

**RAID**（Redundant Array of Independent Disks）是一種將多個獨立硬碟組合起來，形成一個大型邏輯硬碟的技術。RAID 的主要目的在於：

* **提升效能：** 透過將資料分散寫入多個硬碟，可以大幅提升讀寫速度（如 RAID 0）。
* **增加資料容錯能力：** 透過資料備援或同位檢查（Parity），即使其中一個或多個硬碟損壞，資料也不會遺失（如 RAID 1、RAID 5、RAID 6 等）。
* **擴大容量：** 將多個小容量硬碟組合起來，形成一個更大的儲存空間。

RAID 透過硬體或軟體層面，將多個實體硬碟**抽象化為一個高效且安全的「虛擬硬碟」**。在完成了 RAID 配置之後，底層的硬碟組件已經被隱藏，上層的系統看到的只是一個單一的邏輯儲存單元。

---

### Volume Group (卷冊群組)

**Volume Group (VG)** 是**邏輯卷冊管理 (LVM)** 中的一個核心概念。LVM 是一個可以彈性管理儲存空間的工具，它位於 RAID 或實體硬碟之上。在 LVM 中，硬碟或 RAID 陣列被視為「實體卷冊」(Physical Volume, PV)，而這些 PV 可以被加入到一個或多個 VG 中。VG 的主要優勢在於：

* **彈性配置：** VG 可以將多個 PV 的容量匯集起來，形成一個大型的儲存池。您可以從這個池中動態地創建、調整、擴展或縮小「邏輯卷冊」(Logical Volume, LV)。
* **動態擴展：** 當儲存空間不足時，您可以在不中斷服務的情況下，向 VG 中新增 PV（例如，添加一個新的硬碟或 RAID 陣列），並將其容量擴展到現有的邏輯卷冊上。
* **快照功能：** LVM 支援建立邏輯卷冊的快照，以便進行備份或在軟體更新前建立一個恢復點。

簡而言之，**Volume Group 提供了儲存空間的彈性管理**，讓您不必受限於 RAID 陣列的固定大小，而是可以根據實際需求來動態分配和調整空間。

---

### 為什麼依序執行？

這三個步驟形成了一個層層相疊、功能遞進的架構，每一層都提供了上一層所不具備的功能：

1.  **硬碟分區**是物理硬碟的初始切割。
2.  **RAID** 在分區之上，將多個硬碟或分區整合成一個具備高效能和容錯能力的儲存單元。
3.  **Volume Group** 則在 RAID 之上，將這些高效且安全的儲存單元匯集起來，形成一個巨大的容量池，讓儲存空間的配置和管理變得**極具彈性**。

這個依序執行的架構，從底層的物理硬碟開始，逐步向上抽象化，最終提供一個兼具效能、安全和彈性的儲存管理解決方案，這對於需要處理大量資料或需要高可用性的企業級應用來說至關重要。

