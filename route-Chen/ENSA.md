# Enterprise Networking, Security, and Automation (ENSA) Supplemental Module


## Ansible
Using SSH remote contorl devices.

key structure 
- playbook : human readable content cmds `yaml` file.
- inventory : All the controlled devices.
- module : The modules can reuse.

## terraform
Using API remote control devices.

HashiCorp Configuration Language (HCL):JSON and XML

Structure and Syntax: HCL resembles JSON in its use of curly braces { } to define objects and square brackets [ ] for lists. However, HCL introduces resource blocks and attribute assignments, which are more straightforward than the tag-based approach of XML or the strictly quoted keys in JSON.

key structure:
- provider
- resource
- output
- variables, data sources...



### sample ciscoe setting
configure Cisco IOS XE Provider plugin 
```bash
Router1> enable
Router1# configure terminal
Router1(config)# restconf
Router1(config)# netconf-yang
Router1(config)# end
Router1# write memory
```

admin@pc
```bash
terraform init
terraform plan
terraform apply
```


### **RESTCONF and NETCONF**
RESTCONF and NETCONF are network configuration protocols that allow direct interaction with network devices. They provide standardized APIs for secure communication, used to modify device configurations and retrieve operational data.

**Ansible** :  uses YAML for its playbook scripts.

**Python** : A programming language widely used for scripting automation tasks

**Terraform** : Configuration files written in HCL.It can manage API-driven network components like firewalls, load balancers, and some routers and switches. 


## REST Authentication
REST (Representational State Transfer) is currently the most widely used API.





