Option Strict On

Imports System.Text.RegularExpressions

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

Public Class ShippingLabelParseResult

    Public Property RawInput As String
    Public Property CleanedTrackingNumber As String
    Public Property DetectedCarrier As String
    Public Property MatchedRecipient As String
    Public Property WasNormalized As Boolean

End Class

Public Class ShippingLabelParser

    Private Shared ReadOnly whitespacePattern As New Regex("\s+", RegexOptions.Compiled)

    Public Function Parse(input As String, knownRecipients As IEnumerable(Of String)) As ShippingLabelParseResult

        Dim normalizedInput As String = NormalizeWhitespace(input)
        Dim result As New ShippingLabelParseResult With {
            .RawInput = input,
            .CleanedTrackingNumber = normalizedInput,
            .DetectedCarrier = String.Empty,
            .MatchedRecipient = String.Empty,
            .WasNormalized = False
        }

        If String.IsNullOrWhiteSpace(normalizedInput) OrElse
           String.Equals(normalizedInput, UiText.TrackingNumberPlaceholder, StringComparison.Ordinal) Then

            Return result

        End If

        Dim compactInput As String = normalizedInput.Replace(" ", String.Empty).Replace("-", String.Empty)
        Dim bestCandidate As String = GetBestTrackingCandidate(normalizedInput, compactInput)
        Dim carrier As String = DetectCarrier(bestCandidate)
        Dim recipient As String = MatchRecipient(normalizedInput, knownRecipients)

        result.CleanedTrackingNumber = bestCandidate
        result.DetectedCarrier = carrier
        result.MatchedRecipient = recipient
        result.WasNormalized = Not String.Equals(normalizedInput, bestCandidate, StringComparison.Ordinal)

        Return result

    End Function

    Private Function NormalizeWhitespace(input As String) As String

        If String.IsNullOrWhiteSpace(input) Then

            Return String.Empty

        End If

        Return whitespacePattern.Replace(input.Trim(), " ")

    End Function

    Private Function GetBestTrackingCandidate(normalizedInput As String, compactInput As String) As String

        Dim candidates As New List(Of String)()
        candidates.Add(compactInput)

        For Each token As String In normalizedInput.Split(" "c)

            Dim cleanedToken As String = token.Trim().Replace("-", String.Empty)

            If cleanedToken.Length >= 8 Then

                candidates.Add(cleanedToken)

            End If

        Next

        For Each candidate As String In candidates.OrderByDescending(Function(x) x.Length)

            Dim detectedCarrier As String = DetectCarrier(candidate)

            If detectedCarrier <> String.Empty Then

                Return candidate

            End If

        Next

        Return candidates.OrderByDescending(Function(x) x.Length).First()

    End Function

    Private Function DetectCarrier(candidate As String) As String

        If Regex.IsMatch(candidate, "^1Z[0-9A-Z]{16}$", RegexOptions.IgnoreCase) Then

            Return "UPS"

        End If

        If Regex.IsMatch(candidate, "^(JD0?\d{16,18}|\d{20})$", RegexOptions.IgnoreCase) Then

            Return "DHL"

        End If

        If Regex.IsMatch(candidate, "^\d{14}$") Then

            Return "DPD"

        End If

        If Regex.IsMatch(candidate, "^\d{8,11}$") Then

            Return "GLS"

        End If

        If Regex.IsMatch(candidate, "^\d{12,15}$") Then

            Return "FedEx"

        End If

        Return String.Empty

    End Function

    Private Function MatchRecipient(normalizedInput As String, knownRecipients As IEnumerable(Of String)) As String

        Dim bestMatch As String = String.Empty

        For Each knownRecipient As String In knownRecipients

            If String.IsNullOrWhiteSpace(knownRecipient) OrElse knownRecipient.Length < 3 Then

                Continue For

            End If

            If normalizedInput.IndexOf(knownRecipient, StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
               knownRecipient.Length > bestMatch.Length Then

                bestMatch = knownRecipient

            End If

        Next

        Return bestMatch

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

Public Class SettingsFormService

    Public Function ValidateBackupSettings(minutesText As String,
                                           backupPath As String,
                                           ByRef backupMinutes As Integer,
                                           ByRef validationMessage As String) As Boolean

        If String.IsNullOrWhiteSpace(minutesText) OrElse
           String.Equals(minutesText, "Minuten", StringComparison.Ordinal) OrElse
           String.IsNullOrWhiteSpace(backupPath) OrElse
           String.Equals(backupPath, "FilePatch", StringComparison.Ordinal) Then

            validationMessage = "Bitte alle Felder ausfuellen! ;-)"
            Return False

        End If

        If Not Integer.TryParse(minutesText, backupMinutes) OrElse backupMinutes <= 0 Then

            validationMessage = "Bitte eine gueltige Anzahl Minuten eingeben! ;-)"
            Return False

        End If

        validationMessage = String.Empty
        Return True

    End Function

End Class
