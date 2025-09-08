# what are forgot?
1. SSH link

```console
S1# show ip ssh

S1(config)# ip domain-name cisco.com

S1(config)# crypto key generate rsa
How many bits in the modulus [512]: 1024

S1(config)# username admin secret ccna

S1(config)# line vty 0 15
S1(config-line)# transport input ssh
S1(config-line)# login local
S1(config-line)# exit

S1(config)# ip ssh version 2
```

```
R1(config)# service password-encryption
```


history commands


switch 
- collision domain 碰撞區域大小，裝置越多範圍越大，但是全雙工交換器沒有此問題
- switch buffer alleitve Network congestion. 緩衝區域緩解壅塞，高速轉低速可以暫存。


