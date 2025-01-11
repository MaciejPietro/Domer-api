using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace Domer.Domain.Common;


public interface IGuid {}

[StronglyTypedId]
public partial struct ProjectId : IGuid
{
    public static implicit operator ProjectId(Guid guid)
    {
        return new ProjectId(guid);
    }
}

[StronglyTypedId]
public partial struct ProjectDetailsId : IGuid
{
    public static implicit operator ProjectDetailsId(Guid guid)
    {
        return new ProjectDetailsId(guid);
    }
}

[StronglyTypedId]
public partial struct ProjectImageId : IGuid
{
    public static implicit operator ProjectImageId(Guid guid)
    {
        return new ProjectImageId(guid);
    }
}

[StronglyTypedId]
public partial struct HeroId : IGuid
{
    public static implicit operator HeroId(Guid guid)
    {
        return new HeroId(guid);
    }
}

[StronglyTypedId]
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}