$followers = "1488143836597657605"

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAANELaAEAAAAAK5wFN%2BVMCmatRnH7hwZkHB3v3KA%3DCv0QibiqSLzYFFmbldIAByb7wXWKuKvc8rv2S60VlszRaYPwAU")
$headers.Add("Cookie", "ct0=c0b93a352b1ea4fa791226a333803a0b; guest_id=v1%3A164707946344757662")

$response = Invoke-RestMethod "https://api.twitter.com/2/users/$followers/followers" -Method 'GET' -Headers $headers
$array_twitter_followers = $response.data | ConvertTo-Json

Write-Output $array_twitter_followers
