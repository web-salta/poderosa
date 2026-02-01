# Contributing to Poderosa

Thank you for your interest in contributing to Poderosa!

## Prerequisites

### Git Authentication

Before you can push changes to this repository, you must set up proper Git authentication. GitHub has disabled password authentication for security reasons.

### Fixing "Authentication failed" Error

If you encounter this error:
```
remote: Invalid username or token. Password authentication is not supported for Git operations.
fatal: Authentication failed for 'https://github.com/web-salta/poderosa.git/'
```

Follow these steps to fix it:

#### Quick Fix: Switch to SSH

The easiest solution is to switch your remote URL from HTTPS to SSH:

```bash
# Check your current remote URL
git remote -v

# Change from HTTPS to SSH
git remote set-url origin git@github.com:web-salta/poderosa.git

# Verify the change
git remote -v
```

You'll also need to set up SSH keys if you haven't already (see README.md for instructions).

#### Alternative: Use Personal Access Token

If you prefer to keep using HTTPS:

1. Create a Personal Access Token (PAT) on GitHub:
   - Visit: https://github.com/settings/tokens
   - Click "Generate new token (classic)"
   - Select scopes: at minimum, select `repo`
   - Generate token and **save it securely** (you won't be able to see it again)

2. When you push, use your token as the password:
   ```bash
   git push origin main
   # Username: your-github-username
   # Password: <paste-your-token-here>
   ```

3. Configure credential caching to avoid entering it every time:
   ```bash
   # Cache credentials for 1 hour (3600 seconds)
   git config --global credential.helper 'cache --timeout=3600'
   
   # Or store credentials permanently (less secure)
   git config --global credential.helper store
   ```

### Git Configuration Best Practices

```bash
# Set your identity
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"

# Use SSH for all GitHub repositories (recommended)
git config --global url."git@github.com:".insteadOf "https://github.com/"

# Enable credential helper
git config --global credential.helper cache
```

## Workflow

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes
4. Commit your changes: `git commit -am 'Add some feature'`
5. Push to your fork: `git push origin feature/your-feature-name`
6. Create a Pull Request

## Getting Help

If you're still having authentication issues:

1. Verify your SSH key is added to GitHub: `ssh -T git@github.com`
2. Check your remote URL: `git remote -v`
3. Ensure your PAT has the correct scopes if using HTTPS
4. Try clearing cached credentials: `git credential-cache exit`

For more information, see [GitHub's authentication documentation](https://docs.github.com/en/authentication).
