using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace Kompass.Domain.Common;


public interface IGuid {}

[StronglyTypedId]
public partial struct DeviceId : IGuid
{
    public static implicit operator DeviceId(Guid guid)
    {
        return new DeviceId(guid);
    }
}


[StronglyTypedId]
public partial struct FolderId : IGuid
{
    public static implicit operator FolderId(Guid guid)
    {
        return new FolderId(guid);
    }
}

[StronglyTypedId]
public partial struct DocumentId : IGuid
{
    public static implicit operator DocumentId(Guid guid)
    {
        return new DocumentId(guid);
    }
}

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
public partial struct ProjectCreatorId : IGuid
{
    public static implicit operator ProjectCreatorId(Guid guid)
    {
        return new ProjectCreatorId(guid);
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
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}