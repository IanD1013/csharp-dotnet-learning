namespace State;

public interface State
{
    void Handle(Context context);
}