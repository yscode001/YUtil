using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 队列任务管理器
    /// </summary>
    public partial class QueueMag : MonoBehaviourBaseY
    {
        private QueueMag() { }
        public static QueueMag Instance { get; private set; } = null;

        /// <summary>
        /// 队列任务
        /// </summary>
        private readonly QueueTask queueTask = new QueueTask((loginfo) =>
        {
            LogTool.Log(loginfo);
        });

        public void Init()
        {
            Instance = this;
            this.Log("初始化(YFramework)：队列任务管理器(QueueMag)");
        }

        private void OnDestroy()
        {
            this.Log("释放(YFramework)：队列任务管理器(QueueMag)");
            Instance = null;
        }

        private void Update()
        {
            // 定时轮询检测与处理由MonoBehaviour中的Update()函数来驱动
            queueTask.Update();
        }
    }

    #region 队列任务的添加、与重置
    public partial class QueueMag
    {
        /// <summary>
        /// 添加主队列任务(支持延时时间)
        /// </summary>
        /// <param name="callback">队列任务委托</param>
        /// <param name="delayTime">延时时间数值</param>
        /// <param name="timeUnit">延时时间单位</param>
        public void RunOnMainQueue(Action callback, double delayTime = 0, TimeUnit timeUnit = TimeUnit.Millisecond)
        {
            if (callback == null || delayTime < 0) { return; }
            queueTask.AddTimeTask(callback, delayTime, timeUnit);
        }

        /// <summary>
        /// 添加主队列任务(支持延时帧数)
        /// </summary>
        /// <param name="callback">队列任务委托</param>
        /// <param name="delayFrame">延时帧数</param>
        public void RunOnMainQueue1(Action callback, uint delayFrame = 0)
        {
            if (callback == null || delayFrame < 0) { return; }
            queueTask.AddFrameTask(callback, delayFrame);
        }

        public void Reset()
        {
            queueTask.Reset();
        }
    }
    #endregion

    #region 扩展
    public static class QueueMagExt
    {
        /// <summary>
        /// 添加主队列任务(支持延时时间)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="callback"></param>
        /// <param name="delayTime"></param>
        /// <param name="timeUnit"></param>
        public static void RunOnMainQueue(this object obj, Action callback, double delayTime = 0, TimeUnit timeUnit = TimeUnit.Millisecond)
        {
            if (obj == null || callback == null || delayTime < 0) { return; }
            QueueMag.Instance.RunOnMainQueue(callback, delayTime, timeUnit);
        }

        /// <summary>
        /// 添加主队列任务(支持延时帧数)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="callback"></param>
        /// <param name="delayFrame"></param>
        public static void RunOnMainQueue1(this object obj, Action callback, uint delayFrame = 0)
        {
            if (obj == null || callback == null || delayFrame < 0) { return; }
            QueueMag.Instance.RunOnMainQueue1(callback, delayFrame);
        }
    }
    #endregion
}