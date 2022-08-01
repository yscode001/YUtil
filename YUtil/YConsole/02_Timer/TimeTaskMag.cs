﻿// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

using System;
using System.Collections.Concurrent;

namespace YConsole
{
    internal class TaskPack
    {
        public uint taskID;
        public Action<uint> task;
        public TaskPack(uint taskID, Action<uint> task)
        {
            this.taskID = taskID;
            this.task = task;
        }
    }

    /// <summary>
    /// 定时任务管理器
    /// </summary>
    public partial class TimeTaskMag : Singleton<TimeTaskMag>
    {
        /// <summary>
        /// 定时任务
        /// </summary>
        private TimeTask timeTask;
        internal bool IsNull => timeTask == null;

        private readonly ConcurrentQueue<TaskPack> taskQueue = new ConcurrentQueue<TaskPack>();
        private readonly string tasklock = "tasklock";

        /// <summary>
        /// 定时任务初始化设置，设置之后才会生效
        /// </summary>
        /// <param name="taskLog"></param>
        /// <param name="repeatTimeInterval"></param>
        public void InitSetting(Action<string> taskLog, uint repeatTimeInterval)
        {
            timeTask = new TimeTask((loginfo) =>
            {
                taskLog?.Invoke(loginfo);
            }, repeatTimeInterval);
            timeTask.SetTaskHandleThread((task, taskID) =>
            {
                if (task != null)
                {
                    lock (tasklock)
                    {
                        taskQueue.Enqueue(new TaskPack(taskID, task));
                    }
                }
            });
        }

        public override void Tick()
        {
            base.Tick();
            while (taskQueue.Count > 0)
            {
                lock (tasklock)
                {
                    if (taskQueue.TryDequeue(out TaskPack result))
                    {
                        result.task?.Invoke(result.taskID);
                    }
                }
            }
        }
    }

    #region 重置定时任务管理器
    public partial class TimeTaskMag
    {
        public void Reset()
        {
            if (timeTask == null) { return; }
            timeTask.Reset();
        }
    }
    #endregion

    #region 定时任务的添加、替换、与删除
    public partial class TimeTaskMag
    {
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="callback">定时任务委托(参数是定时任务ID)</param>
        /// <param name="delayTime">延时时间数值</param>
        /// <param name="timeUnit">延时时间单位</param>
        /// <param name="repeatCount">重复执行次数，0表示无限重复执行</param>
        /// <returns>定时任务ID</returns>
        public uint AddTimeTask(Action<uint> callback, double delayTime, TimeUnit timeUnit, uint repeatCount)
        {
            if (timeTask == null) { return 0; }
            if (callback == null || delayTime < 0 || (repeatCount > 1 && delayTime <= 0)) { return 0; }
            return timeTask.AddTimeTask(callback, delayTime, timeUnit, repeatCount);
        }

        /// <summary>
        /// 定时任务替换
        /// </summary>
        /// <param name="taskID">定时任务ID</param>
        /// <param name="callback">定时任务委托(参数是定时任务ID)</param>
        /// <param name="delayTime">延时时间数值</param>
        /// <param name="timeUnit">延时时间单位</param>
        /// <param name="repeatCount">重复执行次数，0表示无限重复执行</param>
        /// <returns>是否替换成功</returns>
        public bool ReplaceTimeTask(uint taskID, Action<uint> callback, float delayTime, TimeUnit timeUnit, uint repeatCount)
        {
            if (timeTask == null) { return false; }
            if (callback == null || delayTime < 0 || (repeatCount > 1 && delayTime <= 0)) { return false; }
            return timeTask.ReplaceTimeTask(taskID, callback, delayTime, timeUnit, repeatCount);
        }

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="taskID">定时任务ID</param>
        public void DeleteTimeTask(uint taskID)
        {
            if (timeTask == null) { return; }
            timeTask.DeleteTimeTask(taskID);
        }
    }
    #endregion

    #region 定时帧任务的添加、替换、与删除
    public partial class TimeTaskMag
    {
        /// <summary>
        /// 添加定时帧任务
        /// </summary>
        /// <param name="callback">定时帧任务委托(参数是定时帧任务ID)</param>
        /// <param name="delayFrame">延时多少帧执行</param>
        /// <param name="repeatCount">重复执行次数，0表示无限重复执行</param>
        /// <returns></returns>
        public uint AddFrameTask(Action<uint> callback, uint delayFrame, uint repeatCount)
        {
            if (timeTask == null) { return 0; }
            if (callback == null || delayFrame < 0 || (repeatCount > 1 && delayFrame < 1)) { return 0; }
            return timeTask.AddFrameTask(callback, delayFrame, repeatCount);
        }

        /// <summary>
        /// 定时帧任务替换
        /// </summary>
        /// <param name="taskID">定时帧任务ID</param>
        /// <param name="callback">定时帧任务委托(参数是定时帧任务ID)</param>
        /// <param name="delayFrame">延时多少帧执行</param>
        /// <param name="repeatCount">重复执行次数，0表示无限重复执行</param>
        /// <returns>是否替换成功</returns>
        public bool ReplaceFrameTask(uint taskID, Action<uint> callback, uint delayFrame, uint repeatCount)
        {
            if (timeTask == null) { return false; }
            if (callback == null || delayFrame < 0 || (repeatCount > 1 && delayFrame < 1)) { return false; }
            return timeTask.ReplaceFrameTask(taskID, callback, delayFrame, repeatCount);
        }

        /// <summary>
        /// 删除定时帧任务
        /// </summary>
        /// <param name="taskID">定时帧任务ID</param>
        public void DeleteFrameTask(uint taskID)
        {
            if (timeTask == null) { return; }
            timeTask.DeleteFrameTask(taskID);
        }
    }
    #endregion

    #region 扩展
    public static class TimeTaskMagExt
    {
        public static uint AddTimeTask(this object obj, Action<uint> callback, double delayTime, TimeUnit timeUnit, uint repeatCount)
        {
            if (TimeTaskMag.Instance.IsNull) { return 0; }
            if (obj == null || callback == null || delayTime < 0 || (repeatCount > 1 && delayTime <= 0)) { return 0; }
            return TimeTaskMag.Instance.AddTimeTask(callback, delayTime, timeUnit, repeatCount);
        }

        public static bool ReplaceTimeTask(this object obj, uint taskID, Action<uint> callback, float delayTime, TimeUnit timeUnit, uint repeatCount)
        {
            if (TimeTaskMag.Instance.IsNull) { return false; }
            if (obj == null || callback == null || delayTime < 0 || (repeatCount > 1 && delayTime <= 0)) { return false; }
            return TimeTaskMag.Instance.ReplaceTimeTask(taskID, callback, delayTime, timeUnit, repeatCount);
        }

        public static void DeleteTimeTask(this object obj, uint taskID)
        {
            if (TimeTaskMag.Instance.IsNull) { return; }
            if (obj == null) { return; }
            TimeTaskMag.Instance.DeleteTimeTask(taskID);
        }
        public static uint AddFrameTask(this object obj, Action<uint> callback, uint delayFrame, uint repeatCount)
        {
            if (TimeTaskMag.Instance.IsNull) { return 0; }
            if (obj == null || callback == null || delayFrame < 0 || (repeatCount > 1 && delayFrame < 1)) { return 0; }
            return TimeTaskMag.Instance.AddFrameTask(callback, delayFrame, repeatCount);
        }
        public static bool ReplaceFrameTask(this object obj, uint taskID, Action<uint> callback, uint delayFrame, uint repeatCount)
        {
            if (TimeTaskMag.Instance.IsNull) { return false; }
            if (obj == null || callback == null || delayFrame < 0 || (repeatCount > 1 && delayFrame < 1)) { return false; }
            return TimeTaskMag.Instance.ReplaceFrameTask(taskID, callback, delayFrame, repeatCount);
        }
        public static void DeleteFrameTask(this object obj, uint taskID)
        {
            if (TimeTaskMag.Instance.IsNull) { return; }
            if (obj == null) { return; }
            TimeTaskMag.Instance.DeleteFrameTask(taskID);
        }
    }
    #endregion
}