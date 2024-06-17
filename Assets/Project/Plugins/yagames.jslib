mergeInto(LibraryManager.library, {

	ShowFigureOpenAdvExtern: function () {
		const objName = 'MainMenuSceneDirector';
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					gameInstance.SendMessage(objName, 'OnRewardReceived');
				},
				onClose: () => {
					console.log('Video ad closed.');
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				}
			}
		})
	},

	ShowResurrectAdvExtern: function () {
		const objName = 'LevelSceneDirector';
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					gameInstance.SendMessage(objName, 'OnRewardReceived');
				},
				onClose: () => {
					console.log('Video ad closed.');
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				}
			}
		})
	},
	
	SaveExtern: function (data) {
		var dataString = UTF8ToString(data);
		var json = JSON.parse(dataString);
		player.setData(json).then(() => {
			console.log('data is set');
		});
	},
	
	LoadExtern: function () {
		if (gameInstance === null)
		{
			console.log('GameInstance is NULL');
			return;
		}

		if (player === null)
		{
			console.log('Player is NULL');
			return;
		}

		player.getData().then(_data => {
			const json = JSON.stringify(_data);
			console.log('data is get');
			gameInstance.SendMessage('ProjectContext', 'SetDataFromYandex', json);
		});
	},
	
	SetToLeaderboardExtern: function (value) {
		ysdk.getLeaderboards()
			.then(lb => {
				lb.setLeaderboardScore('BestResult', value);
			});
	}

});