# Git Authentication Quick Reference

## Problem: "Invalid username or token. Password authentication is not supported"

### Cause
GitHub disabled password authentication for Git operations on August 13, 2021. You must use either SSH keys or Personal Access Tokens (PAT).

### Quick Solutions

#### Solution 1: Switch to SSH (Fastest)
```bash
# Run the helper script
./fix-git-auth.sh

# Or manually:
git remote set-url origin git@github.com:web-salta/poderosa.git
```

#### Solution 2: Use Personal Access Token
```bash
# 1. Create token at: https://github.com/settings/tokens
# 2. When pushing, use token as password
git push origin main
# Username: your-github-username
# Password: ghp_your_token_here

# 3. Cache credentials
git config --global credential.helper cache
```

## SSH Key Setup (One-time)

```bash
# 1. Generate SSH key
ssh-keygen -t ed25519 -C "your_email@example.com"

# 2. Start ssh-agent and add key
eval "$(ssh-agent -s)"
ssh-add ~/.ssh/id_ed25519

# 3. Copy public key
cat ~/.ssh/id_ed25519.pub
# Add it to: https://github.com/settings/keys

# 4. Test connection
ssh -T git@github.com
```

## Personal Access Token Setup (One-time)

```bash
# 1. Create token at: https://github.com/settings/tokens
#    - Click "Generate new token (classic)"
#    - Select "repo" scope
#    - Generate and SAVE the token securely

# 2. Configure credential caching
git config --global credential.helper cache

# 3. Next time you push, enter:
#    Username: <your-github-username>
#    Password: <your-token>
```

## Check Your Setup

```bash
# Check remote URL
git remote -v

# If it shows HTTPS:
origin	https://github.com/web-salta/poderosa.git (fetch)
origin	https://github.com/web-salta/poderosa.git (push)
# You need a Personal Access Token

# If it shows SSH:
origin	git@github.com:web-salta/poderosa.git (fetch)
origin	git@github.com:web-salta/poderosa.git (push)
# You need SSH keys configured
```

## Troubleshooting

### "Permission denied (publickey)" with SSH
```bash
# Verify SSH key is added to ssh-agent
ssh-add -l

# If empty, add your key
ssh-add ~/.ssh/id_ed25519

# Test GitHub connection
ssh -T git@github.com
```

### "Authentication failed" with HTTPS
```bash
# Clear cached credentials
git credential-cache exit

# Or clear stored credentials (Linux)
rm ~/.git-credentials

# Try pushing again with new token
git push origin main
```

### Switch Between HTTPS and SSH
```bash
# Check current
git remote -v

# Switch to SSH
git remote set-url origin git@github.com:web-salta/poderosa.git

# Switch to HTTPS
git remote set-url origin https://github.com/web-salta/poderosa.git
```

## Automated Fix

Run the provided script:
```bash
./fix-git-auth.sh
```

This script will:
- Detect your current authentication method
- Offer to switch to SSH
- Test your connection
- Provide specific instructions for your setup

## Additional Resources

- [GitHub SSH Key Documentation](https://docs.github.com/en/authentication/connecting-to-github-with-ssh)
- [GitHub Personal Access Token Documentation](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token)
- [Git Credential Storage](https://git-scm.com/docs/gitcredentials)
