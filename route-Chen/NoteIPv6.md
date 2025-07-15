# Notes of IPv6
- 目次
- 傳播方法
- 名詞區分
- DHCP運作

#IPv6 #GUA #LLA #SLAAC #DHCPv6
## 1. 傳播方法
* unicast - 外部IP
* multicast - 群播、多播
* anycast - ask nearest device with unicast addresses.

## 2. 名詞區分
GUA and  LLA. GUA is global, and LLA is local.
* **GUA:** 
    * 定義: IPv6 `global unicast addresses` (GUAs) are globally unique and routable on the IPv6 internet.
    * 實務: Currently, only GUAs with the first three bits of 001 or `2000::/3` are being assigned, as shown in the figure. end 3fff::/3
* **LLA:** 
    * 定義: An IPv6 link-local address (LLA) enables a device to communicate with other IPv6-enabled devices on the same link and only on that link (subnet).
    * 實務: IPv6 LLAs are in the `fe80::/10` range. The /10 indicates that the first 10 bits are 1111 1110 10xx xxxx. The first hextet has a range of 1111 1110 1000 0000 (fe80) to 1111 1110 1011 1111 (febf). end febf::/10

## 3. Router and DHCP
Client sent RS to DHCPv6 and recieve RA.
* **RA** : The ICMPv6 RA message includes the following:
    * a. **Network prefix and prefix length** - This tells the device which network it belongs to.
    * b. **Default gateway address** - This is an IPv6 LLA, the source IPv6 address of the RA message.
    * c. **DNS addresses and domain name** - These are the addresses of DNS servers and a domain name.

* **SLAAC method** : There are three methods for RA messages:
    * **Method 1: SLAAC** - RA from router and create own interface ID.
    * **Method 2: SLAAC with a stateless DHCPv6 server** - RA from router and ask DHCPv6 where DNS. 
    * **Method 3: Stateful DHCPv6 (no SLAAC)** - gateway from router and ask DHCPv6 for IP. 

### IPv6 Address Configuration Methods Summary
| Configuration Method | **Router RA (Router Advertisement)** | **DHCPv6 messages** |
| :----  | :----  | :---- |
| **1. SLAAC** | Network Prefix, Prefix Length, Default Gateway (Router's Link-Local Address)               | **None** (DHCPv6 not used for any configuration)                                                  |
| **2. SLAAC + Stateless DHCPv6** | Network Prefix, Prefix Length, Default Gateway (Router's Link-Local Address) | DNS Server Addresses (and other "stateless" options)             |
| **3. Stateful DHCPv6 (no SLAAC)** | Default Gateway (Router's Link-Local Address) | Assigned IPv6 Address, DNS Server Addresses (and all other options) |

## multicast
`ff02::1` All-nodes multicast group
`ff02::2` All-routers multicast group