# Unity3D_ARPGDemo

游戏主要包含地图生成、角色动画控制、怪物AI、UI部分。  
Unity版本2019.2.0f  

[百度云下载](https://pan.baidu.com/s/1Hz2ir0WDkia2BpXXMuII2Q)  提取码:fspz  

预览图：

![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/1.jpg)

![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/2.jpg)

![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/3.jpg)

![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/4.jpg)

![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/5.jpg)  
  
  
  
  
  
  
  
地图随机生成：  
1.地图房间预制体及连接通道的准备：  
根据素材大小定好每个体素的体积（可将预制体的体积平均分割），如下图中的体积大小为6的立方体，并为每个体素确定位置。   
![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/map1.jpg)  
为预制体添加出口（连接口），添加空物体确定位置即可。  
![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/map3.jpg)  
制作好所有房间素材后就可以随机生成  
2.生成算法：  
在主循环中生成房间，条件为定义的生成个数。在循环中随机获取保存的素材，将其实例化后对其进行三个测试。  
（1）测试生成房间体素是否和已有房间重合。  
（2）新生成房间出口位置是否被其他房间堵住。  
（3）所有房间未连接的出口是否被生成房间堵住。  
地图生成算法详解可查看源码：[DungenGenerator](https://github.com/YimiCGH/DungenGenerator)及其作者[b站](https://www.bilibili.com/read/cv3322436)   
  
  
角色动画控制  
1.动画状态机Animator的制作  
角色动画分为三部分，分别是三种武器的攻击动画。  
![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/Animator_2.jpg)  
做好动画连接切换条件及切换如何过度等。  
![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/Animator_1.jpg)  
2.状态机的控制（有限状态机）  
为了解决状态机动画切换使用基于状态模式设计的有限状态机。  
[状态机框架参考](https://blog.csdn.net/liaoshengg/article/details/81014770)  
  
  
怪物AI  
行为树插件(Behavior Designer）实现,部分怪物连接图如下  
![image](https://github.com/Aetulier/Unity3D_ARPGDemo/blob/master/Preview%20image/EnemyAI.jpg)    
具体行为代码可查看Scripts/Enemy文件夹下脚本代码。   
由于地图为运行后生成所以所有NavMesh的拓展应用 [NavMeshComponents](https://github.com/Unity-Technologies/NavMeshComponents)  
可以在运行后动态烘焙地图，AI寻路使用NavMesh。  
  
  
UI框架和背包系统  
