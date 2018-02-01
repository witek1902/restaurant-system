namespace OrderManagementSystem.Infrastructure.Command
{
    using Castle.Windsor;
    using NHibernate;
    using Exception;
    using SecurityException = Exception.SecurityException;

    public class CommandRunner
    {
        private readonly IWindsorContainer windsor;

        public CommandRunner(IWindsorContainer windsor)
        {
            this.windsor = windsor;
        }

        public CommandExecutionResult<T> ExecuteCommand<T>(Command<T> cmd)
        {
            ProvideCommonServices(cmd);
            cmd.SetupDependencies(windsor);

            return InternalRun(cmd);
        }

        protected virtual void ProvideCommonServices<T>(Command<T> cmd)
        {
            if (cmd is INeedSession)
            {
                ((INeedSession)cmd).Session = this.windsor.Resolve<ISession>();
            }
        }

        protected virtual CommandExecutionResult<T> InternalRun<T>(Command<T> cmd)
        {
            try
            {
                var decoratedCommand = Decorate(cmd);
                var result = decoratedCommand.Execute();

                return CommandExecutionResult<T>.SuccessResult(result);
            }
            catch (BusinessException businessEx)
            {
                return CommandExecutionResult<T>.FailureResult(businessEx.ErrorCode, businessEx.Message);
            }
            catch (SecurityException securityEx)
            {
                return CommandExecutionResult<T>.FailureResult(SecurityException.SecurityExceptionCode, securityEx.Message);
            }
            catch (System.Exception e)
            {
                return CommandExecutionResult<T>.FailureResult("UnexpectedError", e.Message);
            }
        }

        protected virtual ICommand<T> Decorate<T>(Command<T> cmd)
        {
            if (cmd is INeedAutocommitTransaction)
            {
                var decoratedCmd = new TxDecorator<T>(cmd);
                decoratedCmd.SetupDependencies(windsor);
                return decoratedCmd;

            }
            else
                return cmd;
        }
    }

    public class TxDecorator<T> : Command<T>
    {
        private readonly ICommand<T> component;
        private ISession currentSession;

        public TxDecorator(ICommand<T> component)
        {
            this.component = component;
        }

        public override T Execute()
        {
            using (var tx = currentSession.BeginTransaction())
            {
                T cmdResult = component.Execute();
                tx.Commit();
                return cmdResult;
            }
        }

        public override void SetupDependencies(IWindsorContainer container)
        {
            currentSession = container.Resolve<ISession>();
        }

    }

    public class CommandExecutionResult<T>
    {
        public bool Success { get; protected set; }

        public string ErrorCode { get; protected set; }

        public string MessageForHumans { get; protected set; }

        public T Result { get; protected set; }

        public static CommandExecutionResult<T> SuccessResult(T result)
        {
            return new CommandExecutionResult<T> { Success = true, Result = result };
        }

        public static CommandExecutionResult<T> FailureResult(string errCode, string msg)
        {
            return new CommandExecutionResult<T> { Success = false, ErrorCode = errCode, MessageForHumans = msg };
        }
    }
}