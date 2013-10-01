select distinct  substring(v.ParentPath, 3, charindex('\', substring(v.ParentPath, 4, 50))) as Project, 
c.ChangeSetid, dateadd(hour,-8,c.creationdate) as CheckInDate, c.Comment, u.Identityid,
v.ParentPath, v.ChildItem, v.FullPath, v.versionFrom, v.VersionTo, con.NamePart
into TFSCheckInHistory
from <TFS SERVER>.<TFS COLLECTION>.dbo.tbl_ChangeSet  as c  with (nolock)
 join <TFS SERVER>.<TFS COLLECTION>.dbo.tbl_identity as u  with (nolock) on u.Identityid = c.OwnerId
 join <TFS SERVER>.<TFS COLLECTION>.dbo.tbl_Version as v with (nolock) on v.Versionfrom = c.ChangeSetId 
  join <TFS SERVER>.<TFS COLLECTION>.dbo.Constants con with (nolock) on con.[TeamFoundationId] = u.[TeamFoundationId]
 Where Year(dateadd(hour,-8,c.creationdate)) = @Year