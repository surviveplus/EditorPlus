Public MustInherit Class Macaron

    Public MustOverride Sub ReplaceSelectionText(prepare As Action(Of TextActionsParameters), act As Action(Of TextActionsParameters))

    Public MustOverride Sub ReplaceSelectionParagraphs(prepare As Action(Of TextActionsParameters), act As Action(Of TextActionsParameters))

    Public MustOverride Sub ReplaceSelectionWords(prepare As Action(Of TextActionsParameters), act As Action(Of TextActionsParameters))

End Class
