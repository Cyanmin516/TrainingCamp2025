Linux Servies
1. IP
2. FTP
3. DNS
4. 套件管理



## 3. sample of DNS
```sh
#vim /etc/named.conf
listen-on port 53 {`ip;` 127.0.0.1;};
註解IPv6, `//listen-on-v6`
allow-query {`IP.0/24;` localhost;}; #一個網段
incldue "/etc/named/student.conf"; #las line
:wq

#vim /etc/named/student.conf
zone "studentX.example.com" IN{
    type master;
    file "/var/named/student.zone";
};

#vim /var/named/student.zone 
$TTL 10
@   IN SOA dns.studentX.example.com. root ( ;註解用分號 @為本地 dns最後要加.
    2025001 ;serial number
    1H 
    2D ;try again backoff
    3W ;try until
    10 ;life time
)

@   IN NS dns.studentX.com.


systemctl enable --now named
firewall-cmd --add-service=dns
firewall-cmd --add-service=dns --permanent
dig 
```


## 4. sample of 套件
```sh
cd /etc/yum.repos.d/
vim centos.repo
### IN VIM
[baseos]
name=CentOS Stream $releasever - BaseOS
註解舊設定
#metalink=https://mirrors.centos.org/metalink?repo=centos-baseos-$stream&arch=$basearch&protocol=https,http
設定新站台
baseurl=https://mirror.twds.com.tw/centos-stream/9-stream/BaseOS/x86_64/os/

[appstream]
name=CentOS Stream $releasever - AppStream
#metalink=https://mirrors.centos.org/metalink?repo=centos-appstream-$stream&arch=$basearch&protocol=https,http
baseurl=https://mirror.twds.com.tw/centos-stream/9-stream/AppStream/x86_64/os/
:wq
### END VIM

vim epel.repo
### IN VIM  
[epel]
name=Extra Packages for Enterprise Linux 9 - $basearch
#metalink=https://mirrors.fedoraproject.org/metalink?repo=epel-9&arch=$basearch&infra=$infra&content=$contentdir
baseurl=https://mirror.twds.com.tw/fedora/epel/9/Everything/x86_64/
:wq
### END VIM

vim epel-cisco-openh264.repo
### IN VIM
[epel-cisco-openh264]
enable=0
### END VIM

dnf clean all
yum clean all
#redo install
dnf -y install bind bind-chroot bind-utils

```