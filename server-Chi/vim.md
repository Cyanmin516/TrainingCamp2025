# vim
linux file editor, can use keyboard replace mouse movement

## setting own vim env, vimrc
```
vim ~/.vimrc
i
set nu #行號
set autoindent #自動縮排
esc
:wq
```


### move
using vim to edit file 
|action |key|
|:------|:--|
|down   |`j`|
|up     |`k`|
|left   |`h`|
|right  |`l`|
### mode switch
|change mode|key|
|:----------|:--|
|insert     |`i`|
|append     |`a`|
|chose      |`v`|
|escape to view mode|`esc`|
### action
|action |key|
|:------|:--|
|cut    |`x`|
|copy   |`y`|
|paste  |`p`|
|delete 10 line below |`10dd`|
|paste 18 line below  |`18yy`|


### v
|action     |key|
|:----------|:--|
|suspend    |`Ctrl+Z`|
|jobs list  |`jobs`|
|interupt stop|`Ctrl+C`|


### write file and quit vim
|action     |key|
|:----------|:--|
|save file  |`:w`|
|quit vim   |`:q`|
|do both    |`:wq`|
|quit without save |`:q!`|


## not writeable
if ur filename with a `/` but is not insisnt now.
vim would not create a folder and save file, and pop a warning.




