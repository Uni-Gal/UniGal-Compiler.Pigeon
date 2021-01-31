# UniGal.Compilers
如你所见，这是UniGal-Script的编译器

## 代码结构
UniGal.Compiler.Backend：后端的基础结构  
UniGal.Compiler.BKEBackend：内置后端（之一？）  
UniGal.Compiler.IR：中间表示，支持编译器的基础结构  
UniGal.Compiler.Frontend：编译前端，当然，也可以单独掏出来用  
UniGal.Compiler.Driver：编译组织程序  
UniGal.Compiler.LibDriver：编译组织库

## 为什么是Compilers
照着BKEBackend自己写一个后端，就能支持新的引擎，并不限于官方支持列表  
也就是说有能力你也可以自己支持一个引擎

## 不合惯例的LGPL-2.1是怎么回事
真的用了GPL，那就不好界定什么是“组合使用”  
万一新的引擎要支持Live2D，那你的后端就没法兼容GPL  
但是LGPL好说，只要不修改UniGal.Compiler.Backend和UniGal.Compiler.IR，新的后端就能随便发布

## 那你做完了吗
没有，因为Fa鸽≈鸽

## 那UniGal-Compiler又是怎么回事
那个是早先做出来，能用  
(能用，但不建议用，因为那个只支持纯文本```<text></text>```，并且支持到UniGal-Script V0.0.2（现在已经是V0.1.0了）)
我这还没做完呢
