#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class DateRange
    Public Start As Date
    Public Finish As Date

    ''' <remarks>
    ''' [21/05/2012] MitchellCooper   Created to allow XML serialisation.
    ''' </remarks>
    Public Sub New()
    End Sub

    Public Sub New(ByVal pdteStart As Date, ByVal pdteFinish As Date)
        Me.Start = pdteStart
        Me.Finish = pdteFinish
    End Sub

    Public Sub New(ByVal pstrDateRange As String)
        Me.New(pstrDateRange, -1)
    End Sub

    Public Sub New(ByVal pstrDateRange As String, ByVal pintAppendYear As Integer)
        Dim arrDates As Date() = Nothing

        arrDates = ConvertDateRangeStringToDateArray(pstrDateRange, pintAppendYear)

        If arrDates IsNot Nothing Then
            Me.Start = arrDates(0)
            Me.Finish = arrDates(1)
        Else
            Throw New Exception("Unable to parse DateRange: " & pstrDateRange)
        End If
    End Sub

    Public Shared Function ConvertDateRangeStringToDateArray(ByVal pstrDateRange As String) As Date()
        Return ConvertDateRangeStringToDateArray(pstrDateRange, -1)
    End Function

    ''' <summary>
    ''' Splits the string using 'to' or '-' as seperator and returns dates as an array. Returns nothing
    ''' if unable to convert or split either date.
    ''' </summary>
    ''' <param name="pstrDateRange"></param>
    Public Shared Function ConvertDateRangeStringToDateArray(ByVal pstrDateRange As String, ByVal pintAppendYear As Integer) As Date()
        Dim arrDates As String()

        arrDates = SplitDateRangeStringToStringArray(pstrDateRange, pintAppendYear)

        If arrDates IsNot Nothing Then
            If arrDates.Length <> 2 Then
                Return Nothing
            Else
                If Date.TryParse(arrDates(0), Date.Now) _
                AndAlso Date.TryParse(arrDates(1), Date.Now) Then
                    Return New Date() {CDate(arrDates(0)), CDate(arrDates(1))}
                Else
                    Return Nothing
                End If
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function SplitDateRangeStringToStringArray(ByVal pstrDateRange As String) As String()
        Return SplitDateRangeStringToStringArray(pstrDateRange, -1)
    End Function

    Public Shared Function SplitDateRangeStringToStringArray(ByVal pstrDateRange As String, ByVal pintAppendYear As Integer) As String()
        Dim arrDates As String()

        If pstrDateRange.Contains("to") Then
            pstrDateRange = pstrDateRange.Replace("to", "-")
        End If

        arrDates = pstrDateRange.Split("-"c)

        If arrDates.Length <> 2 Then
            arrDates = pstrDateRange.Split(" "c)
        End If

        If arrDates.Length = 2 Then

            arrDates(0) = arrDates(0).Trim
            arrDates(1) = arrDates(1).Trim

            If pintAppendYear > 0 Then
                arrDates(0) &= " " & pintAppendYear.ToString
                arrDates(1) &= " " & pintAppendYear.ToString
            End If

            Return New String() {arrDates(0), arrDates(1)}
        Else
            Return Nothing
        End If
    End Function

    Public ReadOnly Property TotalDays() As Integer
        Get
            Return CInt(Me.Finish.Subtract(Me.Start).TotalDays)
        End Get
    End Property

    Public Function DoDateRangesOverlap(ByVal pobjDateRange As DateRange) As Boolean
        Return Me.IsDateWithinRange(pobjDateRange.Start) _
            OrElse Me.IsDateWithinRange(pobjDateRange.Finish) _
            OrElse (pobjDateRange.Start <= Me.Start AndAlso pobjDateRange.Finish >= Me.Finish)

    End Function

    Public Function DoDatesOverlapNonInclusive(ByVal pobjDateRange As DateRange) As Boolean
        Return Me.IsDateWithinRange(pobjDateRange.Start) _
            OrElse Me.IsDateWithinRange(pobjDateRange.Finish) _
            OrElse (pobjDateRange.Start < Me.Start AndAlso pobjDateRange.Finish > Me.Finish)

    End Function

    Public Function IsDateWithinRange(ByVal pdteDate As Date) As Boolean
        Return pdteDate >= Me.Start _
        AndAlso pdteDate <= Me.Finish
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Me Then
            Return True
        End If

        If obj.GetType Is GetType(DateRange) Then
            Dim objToCompare As DateRange
            objToCompare = CType(obj, DateRange)

            Return Me.Start = objToCompare.Start _
            AndAlso Me.Finish = objToCompare.Finish

        Else
            Return False
        End If
    End Function

    Public Class DateRangeComparer : Implements IEqualityComparer(Of DateRange)

        Public Function Equals1(ByVal x As DateRange, ByVal y As DateRange) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of DateRange).Equals
            Return x.Equals(y)
        End Function

        ''' <summary>
        ''' Hashcode will be the same for two objects of equal value equality but HashCode equality does not mean
        ''' object value equality.
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetHashCode1(ByVal obj As DateRange) As Integer Implements System.Collections.Generic.IEqualityComparer(Of DateRange).GetHashCode
            Return obj.Start.GetHashCode + obj.TotalDays
        End Function
    End Class
End Class
