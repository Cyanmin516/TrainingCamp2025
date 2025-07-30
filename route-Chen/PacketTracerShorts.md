# Cisco packet tracer
a simulator for internet working with in real device and configuration



## key words 
in commands prompt
`Ctrl+C`    : quit the 

int CLI
`tab` : auto fill the word, if multi choice and display above.
`?`   : connect with word that display the word prefix is matched. Or you can type a space before then display the commands options
`Ctrl+shift+6` :  quit the process 
    
## short terms
Shorts the commands helpping typing time, but lost some complete meaning with few letters.

Keywords just type prefix only.

|shorts|origins|
|:-|:-|
|`en`|enable|
|`show r`|show running-config|
|`conf t`|configure termianal|
|`in g0/0`|interface gigabitEthernet 0/0 |
|`in f0/0`|interface FastEthernet 0/0|
|`in s0/0`|interface Serial0/0|
|`in v 1`|interface vlan 1|
|`ip a *ip *mask`|ip address 127.0.0.1 255.255.255.0|
|`do show r`|in interface but wantting showing the setting of interface|
|`wr`|copy running-config startup-config|
|`do sh ru`|do show running-config|
|`sh ip int b`|show ip interface brief|



|who|shorts|origins|
|:-|:-|:-|
|RTA(config)#|`h RTA`|hostname RTA| 


## show option with | 
1. four keyworkds section, include, exclude, begin

make sure with space at front and back `cmd1 | cmd2`
>sh r|section interface is not working
> sh r | s int is OK

#failed

2. multi keyworks
pipeline with space split commands, but connect with mutli-words
>#sh r | s int|line
`cmd1 | cmd2.1|2.2`


