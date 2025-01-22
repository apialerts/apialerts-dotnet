# Release Process

1. Update the version in `src/ApiAlerts.Common/ApiAlerts.Common.csproj`
2. Update the version in `src/ApiAlerts.Common/Constants.kt`
3. PR to `main` branch, ensure tests pass then merge
4. Create a new release on GitHub on `main`
5. GitHub Actions will publish to Nuget
