cisco name explain and hard words


4.7
---

## 1. UTP 纜線的特性：消除干擾

在 Cisco Networking Academy 的課程中，了解不同網路纜線的特性是基礎。針對 **UTP（Unshielded Twisted Pair）無屏蔽雙絞線**，其最主要的特性是 **消除干擾 (Cancellation)**。

### 什麼是「消除干擾」？

UTP 纜線內部的兩條導線會彼此交錯纏繞。這種獨特的絞合設計會產生一個電磁場，這個電磁場能有效地**抵消**來自外部的電磁干擾 (EMI)，例如日光燈或馬達產生的雜訊。同時，它也能大幅減少**串擾 (Crosstalk)**，也就是纜線內不同線對之間訊號的相互干擾。透過這種方式，訊號的穩定性和傳輸品質都能得到顯著提升。

### 其他選項為何不適用 UTP？

* **Cladding（包覆層）**：這是**光纖電纜**的組成部分，用來引導光線在光纖核心中傳播，與 UTP 無關。
* **Immunity to electrical hazards（對電氣危害的免疫性）**：UTP 纜線本身並沒有高度的電氣危害免疫性，它是由銅線製成，仍可能導電。這種免疫性更接近**光纖電纜**的特性，因為光纖傳輸的是光訊號，而非電訊號。
* **Woven copper braid or metallic foil（編織銅網或金屬箔）**：這些是**屏蔽層**的材料，通常用於 **STP（Shielded Twisted Pair）屏蔽雙絞線** 或同軸電纜，目的是提供額外的電磁干擾防護。而 UTP 的名稱正是指其**沒有**這類金屬屏蔽層。

因此，當談到 UTP 纜線的獨特優勢時，**消除干擾**無疑是最核心且關鍵的特性。

## 2.  rollover
A rollover cable (also known as a console cable)


## 3.IEEE 802 LAN/MAN Data Link Sublayers
C6.1
* **Logical Link Control (LLC)** - This IEEE 802.2 sublayer communicates between the networking software at the upper layers and the device hardware at the lower layers

* **Media Access Control (MAC)** - Implements this sublayer (IEEE 802.3, 802.11, or 802.15) in hardware.

## 4. NICs
C6.3
Network Interface Cards (NICs)

encapsulation 封裝
