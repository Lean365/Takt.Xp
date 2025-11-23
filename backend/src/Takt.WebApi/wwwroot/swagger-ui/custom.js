// Swagger UI 自定义脚本
window.onload = function() {
    // 添加复制按钮到代码块
    function addCopyButtons() {
        const codeBlocks = document.querySelectorAll('pre.body-param__example');
        codeBlocks.forEach(block => {
            const button = document.createElement('button');
            button.className = 'copy-button';
            button.textContent = '复制';
            button.onclick = function() {
                const code = block.querySelector('code').textContent;
                navigator.clipboard.writeText(code).then(() => {
                    button.textContent = '已复制!';
                    setTimeout(() => {
                        button.textContent = '复制';
                    }, 2000);
                });
            };
            block.parentNode.insertBefore(button, block);
        });
    }

    // 添加请求时间统计
    function addRequestTiming() {
        const originalFetch = window.fetch;
        window.fetch = function() {
            const start = performance.now();
            return originalFetch.apply(this, arguments)
                .then(response => {
                    const end = performance.now();
                    const duration = end - start;
                    console.log(`请求耗时: ${duration.toFixed(2)}ms`);
                    return response;
                });
        };
    }

    // 添加请求历史记录
    function addRequestHistory() {
        const history = [];
        const maxHistory = 10;

        function addToHistory(request) {
            history.unshift(request);
            if (history.length > maxHistory) {
                history.pop();
            }
            updateHistoryUI();
        }

        function updateHistoryUI() {
            const container = document.querySelector('.swagger-ui');
            let historyPanel = document.getElementById('request-history');
            if (!historyPanel) {
                historyPanel = document.createElement('div');
                historyPanel.id = 'request-history';
                historyPanel.className = 'request-history-panel';
                container.prepend(historyPanel);
            }

            historyPanel.innerHTML = `
                <h3>最近的请求</h3>
                <ul>
                    ${history.map(req => `
                        <li>
                            <span class="method ${req.method.toLowerCase()}">${req.method}</span>
                            <span class="path">${req.path}</span>
                            <span class="time">${req.time}</span>
                        </li>
                    `).join('')}
                </ul>
            `;
        }

        const originalFetch = window.fetch;
        window.fetch = function() {
            const request = arguments[0];
            if (request.url.includes('/api/')) {
                addToHistory({
                    method: request.method,
                    path: new URL(request.url).pathname,
                    time: new Date().toLocaleTimeString()
                });
            }
            return originalFetch.apply(this, arguments);
        };
    }

    // 添加暗色主题支持
    function addDarkThemeSupport() {
        const themeToggle = document.createElement('button');
        themeToggle.id = 'theme-toggle';
        themeToggle.textContent = '切换主题';
        themeToggle.onclick = function() {
            document.body.classList.toggle('dark-theme');
            localStorage.setItem('swagger-theme', 
                document.body.classList.contains('dark-theme') ? 'dark' : 'light'
            );
        };

        const topbar = document.querySelector('.swagger-ui .topbar');
        topbar.appendChild(themeToggle);

        // 恢复保存的主题设置
        const savedTheme = localStorage.getItem('swagger-theme');
        if (savedTheme === 'dark') {
            document.body.classList.add('dark-theme');
        }
    }

    // 添加请求参数验证
    function addRequestValidation() {
        const originalExecute = SwaggerUIStandalonePreset.apis.execute;
        SwaggerUIStandalonePreset.apis.execute = function(options) {
            // 验证必填参数
            const requiredParams = options.parameters.filter(p => p.required);
            const missingParams = requiredParams.filter(p => !p.value);
            if (missingParams.length > 0) {
                alert(`以下参数为必填项:\n${missingParams.map(p => p.name).join('\n')}`);
                return Promise.reject();
            }

            // 验证参数格式
            const invalidParams = options.parameters.filter(p => {
                if (!p.value) return false;
                switch (p.type) {
                    case 'integer':
                        return isNaN(parseInt(p.value));
                    case 'number':
                        return isNaN(parseFloat(p.value));
                    case 'boolean':
                        return !['true', 'false'].includes(p.value.toLowerCase());
                    default:
                        return false;
                }
            });

            if (invalidParams.length > 0) {
                alert(`以下参数格式无效:\n${invalidParams.map(p => p.name).join('\n')}`);
                return Promise.reject();
            }

            return originalExecute.apply(this, arguments);
        };
    }

    // 初始化所有功能
    setTimeout(() => {
        addCopyButtons();
        addRequestTiming();
        addRequestHistory();
        addDarkThemeSupport();
        addRequestValidation();
    }, 1000);
}; 