# What is this?
简单2D动作游戏demo

# What did I do?
玩家方面：实现了移动（A/D），蹲（S），蹲走，冲刺（Shift），翻滚（Shift），跳（Space）以及多段跳，轻攻击连段（J/Numpad1），跳砸（空中S）（就...Bug挺多的）和冲刺攻击。不过目前只有轻攻击的三段连段有伤害判定；

![玩家状态转移图](/chara_state.png "Player State")

敌人方面：目前只有一个史莱姆，具有游走、靠近敌人和攻击三个状态（外加死亡）；游走状态有概率禁止或者左右移动；攻击有伤害判定；

没了。~~真懒啊，甚至没有写个循环生成敌人的生成器和复活主角的功能!~~

# What have I learned?
## I. Flag Enum
用带`[flag]`前缀的枚举类型，表示角色的状态，方便表示符合状态（例如在空中 + 移动 + 攻击），效果拔群！

    [Flags]
    public enum StateFlags
    {
        none = 0b_0000_0000,
        air = 0b_0000_0001,
        crouch = 0b_0000_0010,
        smash = air | crouch,
        moveLeft = 0b_0000_0100,
        moveRight = 0b_0000_1000,
        mid = moveLeft | moveRight,
        dash = 0b_0001_0000,
        lightAttack = 0b_0010_0000,
        heavyAttack = 0b_0100_0000,
    }

## II. Finite State Machine
学习有限状态机，并且自己写了一个。还是挺复杂的，能跑就是胜利！但是想要在某一个状态里实现多个Animation Event的功能还是没想好要怎么处理。同时，也用了层级有限状态机，但是感觉其实在我这儿用处不大。

## III. Interface
C#接口是好文明，方便抽象逻辑，方便维护、拓展与代码复用，反正咱觉着，在游戏方面，用了都说好！

    foreach(Collider2D hitTarget in _hitTargets)
    {
        if (hitTarget == null) 
            continue;
        _targetMob =  hitTarget.gameObject.GetComponent<MobSlime>() as IDamagable;
        if (_targetMob == null)
            continue;
        _targetMob.TakeDamage(_context.ComboList[_comboCounter].damage);
        //Debug.Log(_targetMob.ToString() + "get Hit!");
    }


## IV. Scriptable Object
可编辑对象，官方说是用于存放数据的容器。在这里，我用于存放连段数据，包括Animation Clip、前摇后摇时间、每一招的伤害、名称、攻击判定hitbox等等。非常方便，易于拓展。既方便给攻击添加新功能，又方便增加新连段、调整连段顺序、更改连段数量等等。真的很酷！

可惜这个idea是咱[借鉴](https://www.youtube.com/watch?v=bjX3Uc02e0g&list=PL1dAupW-QMEG-ESgrWMj9BelyIfoyy96r&index=2&t=817s&ab_channel=TheGameDevCave)的哈哈哈哈哈哈~要是是我自己想出来的就牛咯~

    [CreateAssetMenu(menuName = "Attacks/Light Attack")]
    public class AttackComboSO : ScriptableObject
    {
        public AnimationClip clip;
        public string animationClipName;
        public float interuptGap;
        public float endingTime;

        public Vector2 hitBoxPointA;
        public Vector2 hitBoxPointB;

        public int damage;
    }

## V. Live in the moment
得把期望适当放低，才能推进进度，过早地耽搁在细节里反而不利于成长。
