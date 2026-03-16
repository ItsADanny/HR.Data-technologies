public enum ePermissions
{
    None,
    Write,
    Read,
    ReadWrite
}

public class Permission
{
    private ePermissions _permission;

    public int PermissionInteger
    {
        get
        {
            return (int) _permission;
        }
        set
        {
            _permission = (ePermissions) value;
        }
    }

    //Creating a Premission object (no value inputted)
    public Permission() => new Permission(0);

    //Creating a Premission object (value inputted)
    public Permission(int value) => PermissionInteger = value;

    public bool HasWritePremission() => 
        _permission == ePermissions.Write|| _permission == ePermissions.ReadWrite ? true : false;

    public bool HasReadPremission() => 
        _permission == ePermissions.Read|| _permission == ePermissions.ReadWrite ? true : false;

    public void SetWrite(bool state)
    {
        if (_permission == ePermissions.ReadWrite)
        {   
            if (!state)
            {
                _permission = ePermissions.Read;   
            }
        }
        else
        {
            if (state)
            {
                if (HasReadPremission())
                {
                    _permission = ePermissions.ReadWrite;
                }
                else
                {
                    _permission = ePermissions.Write;
                }
            } 
            else
            {
                _permission = ePermissions.None;
            }
        }
    }

    public void SetRead(bool state)
    {
        if (_permission == ePermissions.ReadWrite)
        {   
            if (!state)
            {
                _permission = ePermissions.Write;
            }
        }
        else
        {
            if (state)
            {
                if (HasWritePremission())
                {
                    _permission = ePermissions.ReadWrite;
                }
                else
                {
                    _permission = ePermissions.Read;
                }
            }
            else
            {
                _permission = ePermissions.None;
            }
        }
    }
}