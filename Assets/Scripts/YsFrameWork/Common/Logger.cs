#define YSLogger

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace YSFramework.Utils
{

/// <summary>
/// E log level.
/// 自定义Log等级
/// </summary>
public enum E_LogLevel
{
    NONE = 0,
    DEBUG = 1,
    INFO = 2,
    WARNING = 4,
    ERROR = 8,
    /// <summary>
    /// The EXCEPTION.
    /// Exception 异常Log
    /// </summary>
    EXCEPTION = 16,
    /// <summary>
    /// The CRITICA.
    /// 重要Log
    /// </summary>
    CRITICAL = 32,
}

/// <summary>
/// Debug util.
/// Debug 工具类
/// DebugUtil.Debug("afjjakglj");
/// </summary>
public class Logger
{
    public static E_LogLevel CurrentLogLevels = E_LogLevel.DEBUG | E_LogLevel.INFO | E_LogLevel.WARNING | E_LogLevel.ERROR | E_LogLevel.CRITICAL | E_LogLevel.EXCEPTION;
    private const Boolean IsShowStack = true;
    private static LogWriter _logWriter;

    static Logger()
    {
        _logWriter = new LogWriter();
        Application.RegisterLogCallback(new Application.LogCallback(ProcessLogMessageReceived));
        Logger.Debug(Application.persistentDataPath);
    }

    public static void Release()
    {
        _logWriter.Release();
    }

    static ulong index = 0;

    /// <summary>
    /// 輸出顏色為橘色的Debug訊息
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="isShowStack">If set to <c>true</c> is show stack.</param>
    /// <param name="user">User.</param>
    public static void Debug(object message, Boolean isShowStack = IsShowStack, int user = 0)
    {
        // 0011 1111(默認為全部包括) & 0000 0001 = 0000 0001
        // 如果當前的logLevels有包含Debug層級,就輸出Debug
        if (E_LogLevel.DEBUG == (CurrentLogLevels & E_LogLevel.DEBUG))
            Log(string.Concat(" <color=orange>[DEBUG]</color>: ", isShowStack ? GetStackInfo() : "", message, " Index = ", index++), E_LogLevel.DEBUG);
    }

    public static void Debug(string filter, object message, Boolean isShowStack = IsShowStack)
    {
        if (E_LogLevel.DEBUG == (CurrentLogLevels & E_LogLevel.DEBUG))
        {
            Log(string.Concat(" <color=orange>[DEBUG]</color>: ", isShowStack ? GetStackInfo() : "", message), E_LogLevel.DEBUG);
        }

    }
    /// <summary>
    /// 輸出顏色為
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="isShowStack">If set to <c>true</c> is show stack.</param>
    public static void Info(object message, Boolean isShowStack = IsShowStack)
    {
        if (E_LogLevel.INFO == (CurrentLogLevels & E_LogLevel.INFO))
            Log(string.Concat(" [INFO]: ", isShowStack ? GetStackInfo() : "", message), E_LogLevel.INFO);
    }

    public static void Warning(object message, Boolean isShowStack = IsShowStack)
    {
        if (E_LogLevel.WARNING == (CurrentLogLevels & E_LogLevel.WARNING))
            Log(string.Concat(" [WARNING]: ", isShowStack ? GetStackInfo() : "", message), E_LogLevel.WARNING);
    }

    public static void Error(object message, Boolean isShowStack = IsShowStack)
    {
        if (E_LogLevel.ERROR == (CurrentLogLevels & E_LogLevel.ERROR))
            Log(string.Concat(" <color=red>[ERROR]</color>: ", message, '\n', isShowStack ? GetStacksInfo() : ""), E_LogLevel.ERROR);
    }

    public static void Critical(object message, Boolean isShowStack = IsShowStack)
    {
        if (E_LogLevel.CRITICAL == (CurrentLogLevels & E_LogLevel.CRITICAL))
            Log(string.Concat(" [CRITICAL]: ", message, '\n', isShowStack ? GetStacksInfo() : ""), E_LogLevel.CRITICAL);
    }

    public static void Except(Exception ex, object message = null)
    {
        if (E_LogLevel.EXCEPTION == (CurrentLogLevels & E_LogLevel.EXCEPTION))
        {
            Exception innerException = ex;
            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }
            Log(string.Concat(" [EXCEPTION]: ", message == null ? "" : message + "\n", ex.Message, innerException.StackTrace), E_LogLevel.CRITICAL);
        }
    }

    private static String GetStacksInfo()
    {
        StringBuilder sb = new StringBuilder();
        StackTrace st = new StackTrace();
        var sf = st.GetFrames();
        for (int i = 2; i < sf.Length; i++)
        {
            sb.AppendLine(sf[i].ToString());
        }

        return sb.ToString();
    }

    private static void Log(string message, E_LogLevel level, bool writeEditorLog = true)
    {
        var msg = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), message);
        _logWriter.WriteLog(msg, level, writeEditorLog);
    }

    private static String GetStackInfo()
    {
        //		StackTrace st1 = new StackTrace();
        //		StackFrame[] sfs = st1.GetFrames();
        //		for (int u = 0; u < sfs.Length; ++u)
        //		{
        //			MethodBase mb = sfs[u].GetMethod();
        //			String.Format("[CALL STACK][{0}]: {1}.{2}", u, mb.DeclaringType.FullName, mb.Name);
        //		}

        StackTrace st = new StackTrace();
        StackFrame sf = st.GetFrame(2);//
        MethodBase method = sf.GetMethod();
        return String.Format("{0}.{1}(): ", method.ReflectedType.Name, method.Name);
    }

    private static void ProcessLogMessageReceived(string message, string stackTrace, LogType type)
    {
        E_LogLevel logLevel = E_LogLevel.DEBUG;
        switch (type)
        {
            case LogType.Assert:
                logLevel = E_LogLevel.DEBUG;
                break;
            case LogType.Error:
                logLevel = E_LogLevel.ERROR;
                break;
            case LogType.Exception:
                logLevel = E_LogLevel.EXCEPTION;
                break;
            case LogType.Log:
                logLevel = E_LogLevel.DEBUG;
                break;
            case LogType.Warning:
                logLevel = E_LogLevel.WARNING;
                break;
            default:
                break;
        }

        // 如果當前的logLevel存在於CurrentLogLevels,便進行Log寫入
        if (logLevel == (CurrentLogLevels & logLevel))
            Log(string.Concat(" [SYS_", logLevel, "]: ", message, '\n', stackTrace), logLevel, false);
    }
}

/// <summary>
/// Log writer.
/// Log 文件寫入器
/// </summary>
public class LogWriter
{
    private string _logPath = UnityEngine.Application.persistentDataPath + "/Log/";
    private string _logFileName = "Log_{0}.txt";
    private string _logFilePath;
    private FileStream _fs;
    private StreamWriter _sw;
    private Action<String, E_LogLevel, bool> _logWriter;
    private readonly static object _locker = new object();

    public LogWriter()
    {
        if (!Directory.Exists(_logPath))
            Directory.CreateDirectory(_logPath);

        _logFilePath = String.Concat(_logPath, String.Format(_logFileName, DateTime.Today.ToString("yyyyMMdd")));

        try
        {
            _logWriter = Write;
            _fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            _sw = new StreamWriter(_fs);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError(ex.Message);
        }
    }

    public void Release()
    {
        lock (_locker)
        {
            if (_sw != null)
            {
                _sw.Close();
                _sw.Dispose();
            }
            if (_fs != null)
            {
                _fs.Close();
                _fs.Dispose();
            }
        }
    }

    public void WriteLog(string msg, E_LogLevel level, bool writeEditorLog)
    {
#if UNITY_IPHONE
		_logWriter(msg, level, writeEditorLog);
#else
        _logWriter.BeginInvoke(msg, level, writeEditorLog, null, null);
#endif
    }

    private void Write(string msg, E_LogLevel level, bool writeEditorLog)
    {
        lock (_locker)
            try
            {
                if (writeEditorLog)
                {
                    switch (level)
                    {
                        case E_LogLevel.DEBUG:
                        case E_LogLevel.INFO:
                            UnityEngine.Debug.Log(msg);
                            break;
                        case E_LogLevel.WARNING:
                            UnityEngine.Debug.LogWarning(msg);
                            break;
                        case E_LogLevel.ERROR:
                        case E_LogLevel.EXCEPTION:
                        case E_LogLevel.CRITICAL:
                            UnityEngine.Debug.LogError(msg);
                            break;
                        default:
                            break;
                    }
                }
                if (_sw != null)
                {
                    _sw.WriteLine(msg);
                    _sw.Flush();
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(ex.Message);
            }
    }
}
}
