Public Module UiText

    Public Const SenderPlaceholder As String = "Sender"
    Public Const RecipientPlaceholder As String = "Empfänger"
    Public Const TrackingNumberPlaceholder As String = "SendungsNummer"
    Public Const PagePlaceholder As String = "Seite"
    Public Const AllPagesLabel As String = "Alle"

    Public Const NamePlaceholder As String = "Name"
    Public Const DropOffPlaceholder As String = "Abladestelle"
    Public Const ClientPlaceholder As String = "Mandant"

    Public Const EasterEggTrigger As String = "Poststelle"

End Module

Public Class PackageFormService

    Public Function BuildFilter(dateFilterEnabled As Boolean,
                                selectedDate As String,
                                selectedPage As String,
                                sender As String,
                                recipient As String,
                                trackingNumber As String,
                                includePrinted As Boolean) As PackageFilter

        Return New PackageFilter With {
            .Datum = If(dateFilterEnabled, selectedDate, "%"),
            .Mandant = ToPageValue(selectedPage),
            .Sender = ToLikeValue(sender, UiText.SenderPlaceholder),
            .Empfaenger = ToLikeValue(recipient, UiText.RecipientPlaceholder),
            .SendungsNummer = ToLikeValue(trackingNumber, UiText.TrackingNumberPlaceholder),
            .Gedruckt = If(includePrinted, "%", "0")
        }

    End Function

    Public Function IsPackageEntryComplete(sender As String,
                                           recipient As String,
                                           trackingNumber As String) As Boolean

        Return Not IsPlaceholderOrEmpty(sender, UiText.SenderPlaceholder) AndAlso
               Not IsPlaceholderOrEmpty(recipient, UiText.RecipientPlaceholder) AndAlso
               Not IsPlaceholderOrEmpty(trackingNumber, UiText.TrackingNumberPlaceholder)

    End Function

    Public Function NeedsRecipientPrompt(recipient As String, knownRecipients As IEnumerable(Of String)) As Boolean

        If IsPlaceholderOrEmpty(recipient, UiText.RecipientPlaceholder) Then

            Return False

        End If

        For Each knownRecipient As String In knownRecipients

            If String.Equals(knownRecipient, recipient, StringComparison.Ordinal) Then

                Return False

            End If

        Next

        Return True

    End Function

    Public Function CreatePackageRecord(recipient As RecipientRecord,
                                        selectedDate As String,
                                        sender As String,
                                        trackingNumber As String) As PackageRecord

        Return New PackageRecord With {
            .Mandant = recipient.Mandant,
            .Datum = selectedDate,
            .Abladestelle = recipient.Abladestelle,
            .Sender = sender,
            .SendungsNummer = trackingNumber,
            .Empfaenger = recipient.Name,
            .Unterschrift = "                                                           ",
            .Gedruckt = "0"
        }

    End Function

    Public Function IsEasterEggTriggered(sender As String, recipient As String, trackingNumber As String) As Boolean

        Return String.Equals(sender, UiText.EasterEggTrigger, StringComparison.Ordinal) AndAlso
               String.Equals(recipient, UiText.EasterEggTrigger, StringComparison.Ordinal) AndAlso
               String.Equals(trackingNumber, UiText.EasterEggTrigger, StringComparison.Ordinal)

    End Function

    Private Function ToPageValue(selectedPage As String) As String

        If String.IsNullOrWhiteSpace(selectedPage) OrElse
           String.Equals(selectedPage, UiText.PagePlaceholder, StringComparison.Ordinal) OrElse
           String.Equals(selectedPage, UiText.AllPagesLabel, StringComparison.Ordinal) Then

            Return "%"

        End If

        Return selectedPage

    End Function

    Private Function ToLikeValue(text As String, placeholder As String) As String

        If IsPlaceholderOrEmpty(text, placeholder) Then

            Return "%"

        End If

        Return "%" & text & "%"

    End Function

    Private Function IsPlaceholderOrEmpty(text As String, placeholder As String) As Boolean

        Return String.IsNullOrWhiteSpace(text) OrElse String.Equals(text, placeholder, StringComparison.Ordinal)

    End Function

End Class

Public Class RecipientFormService

    Public Function BuildFilter(name As String,
                                abladestelle As String,
                                mandant As String) As RecipientFilter

        Return New RecipientFilter With {
            .Name = ToLikeValue(name, UiText.NamePlaceholder),
            .Abladestelle = ToLikeValue(abladestelle, UiText.DropOffPlaceholder),
            .Mandant = ToLikeValue(mandant, UiText.ClientPlaceholder)
        }

    End Function

    Public Function IsRecipientEntryComplete(name As String,
                                             abladestelle As String,
                                             mandant As String) As Boolean

        Return Not IsPlaceholderOrEmpty(name, UiText.NamePlaceholder) AndAlso
               Not IsPlaceholderOrEmpty(abladestelle, UiText.DropOffPlaceholder) AndAlso
               Not IsPlaceholderOrEmpty(mandant, UiText.ClientPlaceholder)

    End Function

    Private Function ToLikeValue(text As String, placeholder As String) As String

        If IsPlaceholderOrEmpty(text, placeholder) Then

            Return "%"

        End If

        Return "%" & text & "%"

    End Function

    Private Function IsPlaceholderOrEmpty(text As String, placeholder As String) As Boolean

        Return String.IsNullOrWhiteSpace(text) OrElse String.Equals(text, placeholder, StringComparison.Ordinal)

    End Function

End Class
