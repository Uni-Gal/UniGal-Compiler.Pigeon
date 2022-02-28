# UniGal-Compiler.Pigeon

如你所见，这是一个UniGal-Script的编译器，主要由@Akarinnnnn （Fa鸽）维护。

但它将不再适用于大于[0.1.2](https://github.com/Uni-Gal/UniGal-Script/releases/tag/0.1.2)版本的UniGal-Script

## 代码结构
UniGal.Compiler.Backend：后端的基础结构  
UniGal.Compiler.BKEBackend：内置后端（之一？）  
UniGal.Compiler.IR：中间表示，支持编译器的基础结构  
UniGal.Compiler.Frontend：编译前端，当然，也可以单独掏出来用  
UniGal.Compiler.Driver：命令行驱动器  
UniGal.Compiler.LibDriver：驱动器  
UniGal.Compiler.FakeBackend：测试用的假后端

## 为什么是Compilers
照着BKEBackend自己写一个后端，就能支持新的引擎，并不限于官方支持列表  
也就是说有能力你也可以自己支持一个引擎

## 不合惯例的LGPL-2.1是怎么回事
真的用了GPL，那就不好界定什么是“组合使用”  
万一新的引擎要支持Live2D，那你的后端就没法兼容GPL  
但是LGPL好说，只要不修改UniGal.Compiler.Backend和UniGal.Compiler.IR，新的后端就能随便发布

## 那你做完了吗
没有，因为Fa鸽≈鸽
但是应该没了

## 那UniGal-Compiler.Cheese又是怎么回事

那个是早先做出来，能用  

能用，但不建议用，因为那个只支持纯文本```<text></text>```，并且仅支持到UniGal-Script V0.0.2
