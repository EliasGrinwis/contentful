
#CORRECT
$followers = "1488143836597657605"

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAANELaAEAAAAANsF2y1aOzi1URzYP%2BlBEqm6SasI%3DDZAg2pzCdexxOaxfMhcw8cRRKimZVNlT0EVXaYp9VLBHzKZU0x")
$headers.Add("Cookie", "ct0=c0b93a352b1ea4fa791226a333803a0b; guest_id=v1%3A164707946344757662")

$response = Invoke-RestMethod "https://api.twitter.com/2/users/$followers/followers" -Method 'GET' -Headers $headers
$array_twitter_followers = $response | ConvertTo-Json


Write-Output($array_twitter_followers)
