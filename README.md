# Identity Provider Configurer

**In a *work in progress* state!**

## What IdpConfigurer IS about

Configures OpenID Connect and OAuth 2.0 Identity Provider (Idp) services. That is the parameters that decide which clients can use the services and how, this includes:
- Secrets needed by clients to get access to the Idp services
- Redirect uris back to the clients
- Grant / flow the clients can use (i.e authorization flows and / or client credential flow)
- Scopes (i.e api access) includet in access tokens

## What IdpConfigurer is NOT about

- Defining users and roles, this is the domain of the Idp services or applications they give access to.
