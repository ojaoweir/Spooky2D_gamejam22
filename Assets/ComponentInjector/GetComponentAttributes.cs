using System;

namespace IsakUtils
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ComponentAttribute : Attribute
    {

    }

    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ComponentInParentAttribute : Attribute
    {

    }

    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ComponentsInParentAttribute : Attribute
    {

    }

    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ComponentInChildrenAttribute : Attribute
    {

    }

    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ComponentsInChildrenAttribute : Attribute
    {

    }
}